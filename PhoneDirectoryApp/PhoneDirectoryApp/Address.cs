using System;
namespace PhoneDirectoryLibrary
{
    public class Address
    {
        //Member Variables. 
        private int buildingNumber;
        private string streetName;
        private string city;
        private string state;
        private int zipcode;

        //Default constructor. Adds values to the object. 
        public Address()
        {
            Console.WriteLine($"What is the building number?");
            buildingNumber = getIntInput();
            Console.WriteLine($"What is the street name?");
            streetName = Console.ReadLine();
            Console.WriteLine($"What is the city?");
            city = Console.ReadLine();
            Console.WriteLine($"What is the state?");
            state = Console.ReadLine();
            Console.WriteLine($"What is the zipcode?");
            zipcode = getIntInput(); 
        }

        //Overloaded constructor. Assigns values to the object. 
        public Address(int bn, string sn, string cit, string sta, int zip)
        {
            buildingNumber = bn;
            streetName = sn;
            city = cit;
            state = sta;
            zipcode = zip;
        }

        //Simple function for reading number inputs. 
        private int getIntInput()
        {
            int temp; 
            do { try {
                    temp = Int32.Parse(Console.ReadLine());
                    return temp;
                } catch (Exception e) //In order to catch a ArgumentNullException or a FormatException. 
                { Console.WriteLine($"Hm, that isn't right. Try a number please.. "); }
            } while (true);
        }

        //Property values for the member variables. 
        public int GetBuildNum { get { return buildingNumber; } }
        public string GetStreetName { get { return streetName; } }
        public string GetCity { get { return city; } }
        public string GetState { get { return state; } }
        public int GetZipcode { get { return zipcode; } }

        //Simple function that prints in the right order. 
        public string Print() { return $"Address: {buildingNumber} {streetName}, {city}, {state} {zipcode}";  }
    }
}
