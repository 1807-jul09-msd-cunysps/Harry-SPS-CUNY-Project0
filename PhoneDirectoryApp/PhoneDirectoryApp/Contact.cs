/*
 *  If I can Serialize each class within the Contact class,
 *  in addition to the Contact class after, in different 
 *  directories, then I might not need the ContactEntry
 *  class. 
 */

using System;

namespace PhoneDirectoryLibrary
{
    public class Contact
    {
        //Member Variables. 
        private string fName; 
        private string lName;
        private Address address;
        private PhoneNumber phonenum;

        //Property values for the member variables. 
        public string GetFirstName { get { return fName; } }
        public string GetLastName { get { return lName; } }
        public string GetAddressInfo { get { return address.Print(); } } 
        public string GetPhoneNumberInfo { get { return phonenum.Print(); } } 
        public Address GetAddress { get { return address; } } 
        public PhoneNumber GetPhoneNumber { get { return phonenum; } }

        //Default constructor. Adds values to the object. 
        public Contact()
        {
            Console.WriteLine($"What is the first name?");
            fName = Console.ReadLine(); 
            Console.WriteLine($"What is the last name?");
            lName = Console.ReadLine(); 
            address = new Address();
            phonenum = new PhoneNumber(); 
        }

        //Overloaded constructor. Assigns values to the object. 
        public Contact(string fn, string ln)
        {
            fName = fn;
            lName = ln;
            address = new Address();
            phonenum = new PhoneNumber(); 
        }

        //Overloaded constructor. Assigns values to the object. 
        public Contact(string fn, string ln, Address add, PhoneNumber pn) 
        {
            fName = fn;
            lName = ln;
            address = new Address(add.GetBuildNum, add.GetStreetName, add.GetCity, add.GetState, add.GetZipcode);
            phonenum = new PhoneNumber(pn.CCode, pn.ACode, pn.PCode, pn.ECode); 
        }

        //Methods for updating certain data members. 
        public void ChangeFirstName(string newFName) { fName = newFName; } 
        public void ChangeLastName(string newLName) { lName = newLName; } 
        public void ChangePhoneNumber() { phonenum = new PhoneNumber(); } 
        public void ChangeAddress() { address = new Address(); }

        //Simple function that prints in the right order. 
        public string Print() { return $"Name: {lName}, {fName}. \n" + phonenum.Print() + "\n" + address.Print(); }
    }
}
