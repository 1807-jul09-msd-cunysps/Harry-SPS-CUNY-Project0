using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PhoneDirectoryApp
{
    public class OutsideConnect
    {
        //Directory for reading/writing. Perhaps a method for changing directory?
        private static string directory = "C:\\Users\\mexic\\Documents\\temp\\Example.txt";
        private static string connectionString = "Server=tcp:cuny-reva-server.database.windows.net,1433;Initial Catalog=Harry_DB;Persist Security Info=False;User ID=Harry;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        private static string loadStringQuery = "SELECT * FROM DirectoryTable";
        private static string delStringQuery = "DELETE FROM DirectoryTable";
        private static string saveStringQuery = "INSERT INTO DirectoryTable (JsonData) VALUES(Convert(VARBINARY(MAX), '";
        private static string saveStringQuery_2 = "'));";

        public static void WriteToFile(string content) { System.IO.File.WriteAllText(directory, content); }
        public static string ReadFromFile() { return System.IO.File.ReadAllText(directory); }

        public static string ReadFromSQL_DB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(loadStringQuery, connection);
                try
                {
                    connection.Open();
                    byte[] hash = (byte[])command.ExecuteScalar();
                    return Encoding.ASCII.GetString(hash);
                }
                catch (Exception e) { Console.WriteLine($"Error {e.ToString()}"); }
            }
            return null;
        }

        public static void WriteToSQL_DB(string jsonString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string fullSaveQuery = saveStringQuery + jsonString + saveStringQuery_2;
                SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    connection.Open(); 
                    adapter.InsertCommand =new SqlCommand(delStringQuery, connection);
                    adapter.InsertCommand.ExecuteNonQuery(); 
                    adapter.InsertCommand =new SqlCommand(fullSaveQuery, connection);
                    adapter.InsertCommand.ExecuteNonQuery(); 
                } catch (Exception e) { Console.WriteLine($"Error {e.ToString()}"); }

            }
        }
    }
}
