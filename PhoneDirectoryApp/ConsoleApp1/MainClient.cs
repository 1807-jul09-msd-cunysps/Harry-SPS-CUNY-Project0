using System;
using System.Collections.Generic;
using System.Text;
using PhoneDirectoryApp;
using PhoneDirectoryLibrary;

namespace PhoneDirectoryClient
{
    class MainClient
    {
        static void Main(string[] args)
        {
            bool flag = false;
            //Need a save
            //Need a load
            //Read from where? SQL or TEXT
            ContactManager manager = new ContactManager();
            do
            {
                Console.WriteLine($"\n\n\n");
                Console.WriteLine($"What would you like to do?");
                Console.WriteLine($"Enter 'N' to quit. ");
                Console.WriteLine($"1: Load a directory. ");
                Console.WriteLine($"2: Save directory. ");
                Console.WriteLine($"3: Add contact to the directory. ");
                Console.WriteLine($"4: Delete a contact from the directory. ");
                Console.WriteLine($"5: Search for a contact. ");
                Console.WriteLine($"6: Print the current directory. ");
                Console.WriteLine($"7: Update a contact's information. ");
                Console.WriteLine($"8: Switch loading location. ");
                string feedback = Console.ReadLine();
                if (feedback.Contains("1")) { manager.READ(); }
                if (feedback.Contains("2")) { manager.SAVE(); }
                if (feedback.Contains("3")) { manager.ADD(manager.CREATE()); }
                if (feedback.Contains("4")) { manager.DELETE(); }
                if (feedback.Contains("5")) { manager.SEARCH(); }
                if (feedback.Contains("6")) { manager.PRINT(); }
                if (feedback.Contains("7")) { manager.UPDATE();  }
                if (feedback.Contains("8")) { manager.SWITCHLOAD();  }
                if (feedback.Contains("N") || feedback.Contains("n")) { flag = true; }
            } while (flag == false); 
        }
    }
}

