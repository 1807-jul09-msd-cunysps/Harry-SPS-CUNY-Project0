using PhoneDirectoryApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization; 


namespace PhoneDirectoryLibrary
{
    public class ContactManager
    {
        //
        private List<ContactEntry> phonedir;
        private bool SQLSAVE;
        private bool TEXTSAVE; 

        //Default constructor. Initializes the List of ContactEntries for a PhoneDirectory. 
        public ContactManager() { phonedir = new List<ContactEntry>(); SQLSAVE = true; TEXTSAVE = false; }

        //PRINTS the contents of the PhoneDirectory in the right order. 
        public void PRINT() { foreach (ContactEntry cont in phonedir) { Console.WriteLine(cont.Print + "\n"); } }

        //CREATES a new Contact type object. 
        public Contact CREATE() { return new Contact(); }

        //READS a PhoneDirectory from file. 
        public void READ() { StartDeserialization(LoadFile()); }

        //READS a PhoneDirectory from file. 
        public void SAVE() { SaveFile(StartSerialization()); }

        //ADDS a ContactEntry to the PhoneDirectory using a Contact
        public void ADD(Contact cont) { DirectoryEntry(cont); }

        //DELETS a ContactEntry from the PhoneDirectory by using its ID Number. 
        public void SWITCHLOAD()
        {
            bool acceptable = false;
            string feedback; 
            do
            {
                Console.WriteLine($"In order to erase we are going to have to select an entry. ");
                Console.WriteLine($"1: from TEXT file. ");
                Console.WriteLine($"2: from SQL DB. ");
                feedback = Console.ReadLine();
                if (feedback.Contains("1")) { TEXTSAVE = true; SQLSAVE = false; acceptable = true; }
                if (feedback.Contains("2")) { SQLSAVE = true; TEXTSAVE = false; acceptable = true ; }
            } while (acceptable == false);
            if (TEXTSAVE == true) { Console.WriteLine($"We are operating from TEXT. "); }
            if (SQLSAVE == true) { Console.WriteLine($"We are operating from SQL. "); }
        }

        //DELETS a ContactEntry from the PhoneDirectory by using its ID Number. 
        public void DELETE()
        {
            Console.WriteLine($"In order to erase we are going to have to select an entry. ");
            Console.WriteLine($"Who would you like to erase?");
            ContactEntry entry = SEARCH(); 

            for (int i = 0; i < phonedir.Count; i++) 
            {
                if (phonedir[i] == entry)
                {
                    Console.WriteLine($"Entry {entry.Name} has been deleted. ");
                    phonedir.Remove(phonedir[i]);
                    return;
                }
            }
        }

        //UPDATES a ContactEntry in the PhoneDirectory by using its ID Number. 
        public void UPDATE()
        {
            bool acceptable = false; 
            Console.WriteLine($"In order to update we are going to have to select an entry. ");
            Console.WriteLine($"Who would you like to update?");
            ContactEntry entry = new ContactEntry();
            entry = SEARCH();

            Console.WriteLine($"What would you like to update?");
            Console.WriteLine($"1: Name. ");
            Console.WriteLine($"2: Address. ");
            Console.WriteLine($"3: Phone number. ");
            string searchoption = Console.ReadLine();

            do
            {
                if (searchoption.Contains("1")) { UpdateEntryName(entry); acceptable = true; break; }
                if (searchoption.Contains("2")) { UpdateEntryAddress(entry); acceptable = true; break; }
                if (searchoption.Contains("3")) { UpdateEntryPhone(entry); acceptable = true; break; }
                if (searchoption.Contains("return")) { acceptable = true; break; }
                else
                {
                    Console.WriteLine($"Hm. That Doesn't seem right. Try again: ");
                    searchoption = Console.ReadLine();
                }
            } while (acceptable == false );
        }

        //SEARCHES for a ContactEntry in the PhoneDirectory to present their information. 
        public ContactEntry SEARCH()
        {
            Console.WriteLine($"What would you like to search by?");
            Console.WriteLine($"1: Name. ");
            Console.WriteLine($"2: Address. ");
            Console.WriteLine($"3: Phone number. ");
            string searchoption = Console.ReadLine();

            do
            {
                if (searchoption.Contains("1")) { return SearchByName(); }
                if (searchoption.Contains("2")) { return SearchByAddress(); }
                if (searchoption.Contains("3")) { return SearchByPhone(); }
                if (searchoption.Contains("return")) { return null; }
                else
                {
                    Console.WriteLine($"Hm. That Doesn't seem right. Try again: ");
                    searchoption = Console.ReadLine();
                }
            } while (true); 
        }

        private ContactEntry SearchByName()
        {
            Console.WriteLine($"What would you like to search for?");
            string feedback = Console.ReadLine(); 
            foreach (ContactEntry entry in phonedir)
            {
                if (entry.Name.Contains(feedback))
                {
                    Console.WriteLine(entry.Print);
                    return entry;
                }
            }
            return null;
        }

        private ContactEntry SearchByAddress()
        {
            Console.WriteLine($"What would you like to search for?");
            string feedback = Console.ReadLine();
            foreach (ContactEntry entry in phonedir)
            {
                if (entry.Address.Contains(feedback))
                {
                    Console.WriteLine(entry.Print);
                    return entry;
                }
            }
            return null;
        }

        private ContactEntry SearchByPhone()
        {
            Console.WriteLine($"What would you like to search for?");
            string feedback = Console.ReadLine();
            foreach (ContactEntry entry in phonedir)
            {
                if (entry.Number.Contains(feedback))
                {
                    Console.WriteLine(entry.Print);
                    return entry;
                }
            }
            return null;
        }

        private void UpdateEntryPhone(ContactEntry entry)
        {
            ContactEntry newEntry = new ContactEntry();
            newEntry.Name = entry.Name;
            newEntry.Address = entry.Address;
            PhoneNumber pn = new PhoneNumber();
            newEntry.Number = pn.Print();
            phonedir.Remove(entry);
            phonedir.Add(newEntry);
        }

        private void UpdateEntryAddress(ContactEntry entry)
        {
            ContactEntry newEntry = new ContactEntry();
            newEntry.Name = entry.Name;
            newEntry.Number = entry.Number;
            Address add = new Address();
            newEntry.Address = add.Print();
            phonedir.Remove(entry);
            phonedir.Add(newEntry);
        }

        private void UpdateEntryName(ContactEntry entry)
        {
            if (entry == null) { Console.WriteLine($"Error, there is no entry like so!"); return; }
            ContactEntry newEntry = new ContactEntry();
            newEntry.Number = entry.Number;
            newEntry.Address = entry.Address;
            Console.WriteLine($"What is the first name?");
            string tempFName = Console.ReadLine();
            Console.WriteLine($"What is the last name?");
            string tempLName = Console.ReadLine();
            newEntry.Name = tempLName + ", " + tempFName;
            phonedir.Remove(entry);
            phonedir.Add(newEntry);
        }

        private string StartSerialization() { return SerializerJSON.JsonSerializer<List<ContactEntry>>(phonedir);  }

        private void StartDeserialization(string jsonstring) { phonedir = SerializerJSON.JsonDeserialize<List<ContactEntry>>(jsonstring); }

        private void SaveFile(string jsonstring)
        {
            if (SQLSAVE == true) { OutsideConnect.WriteToSQL_DB(jsonstring); }
            if (TEXTSAVE == true) { OutsideConnect.WriteToFile(jsonstring); }
        }

        private string LoadFile()
        {
            if (SQLSAVE == true) { return OutsideConnect.ReadFromSQL_DB(); }
            if (TEXTSAVE == true) { return OutsideConnect.ReadFromFile(); }
            return null; 
        }

        private void DirectoryEntry(Contact cont)
        {
            ContactEntry entry = new ContactEntry();
            entry.Name =  cont.GetLastName + ", " + cont.GetFirstName + ".";
            entry.Address = cont.GetAddressInfo + ".";
            entry.Number = cont.GetPhoneNumberInfo + ".";
            phonedir.Add(entry);
        }
    }
}