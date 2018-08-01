using System;

namespace PhoneDirectoryApp
{
    public class ContactEntry
    {
        //Data members. 
        private string name;
        private string address;
        private string number;
        //Data properties. 
        public string Name { set => name = value; get => name; }
        public string Address { set => address = value; get => address; }
        public string Number { set => number= value; get => number;  }
        //Print property. 
        public string Print { get => $"{name} {address} {number}"; }
    }
}
