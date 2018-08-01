using System;
using System.Collections.Generic;
using PhoneDirectoryLibrary; 
using System.Web.Script.Serialization;

namespace PhoneDirectoryApp
{
    public class SerializerJSON
    {
        //Method for serializing in Json, which also returns a Json in string. 
        public static string JsonSerializer<T>(T t) { return new JavaScriptSerializer().Serialize(t); }

        //Method for deserializing in Json, which also returns that same T object. 
        public static T JsonDeserialize<T>(string jsonstring) { return new JavaScriptSerializer().Deserialize<T>(jsonstring); }

    }
}