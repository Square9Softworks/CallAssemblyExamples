using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Encompass2018
{
    public class TwilioConfigFile
    {
        private string configFilePath = @"C:\Getsmart\TwilioConfigFile.json";
        public TwilioConfigFile(string FilePath = "")
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                configFilePath = FilePath;
            }

        }
        public TwilioConfig GetConfig()
        {
            TwilioConfig config = null;
            try
            {
                JsonConvert.DeserializeObject<TwilioConfig>(File.ReadAllText(configFilePath));
            }
            catch
            {
                //do nothing, flow above this will handle a clean return
            }
            return config;
        }
    }
    public class TwilioConfig
    {
        //ProcessField IDs that are passed from GlobalAction to use as targets for our notification
        public List<int> NotifyFields { get; set; }
        //the message to send in our notification
        public string CustomMessage { get; set; }
        //our phonebook of user contacts
        public Dictionary<string, User> Phonebook { get; set; }
    }
    public class User
    {
        public User(int id, string name, string phone)
        {
            ID = id;
            Name = name;
            PhoneNumber = phone;
        }
        //an ID for easy reference to our user
        public int ID { get; set; }
        //User Name
        public string Name { get; set; }
        //User Phone number, must be stored with country code e.g. "+12345678901"
        public string PhoneNumber { get; set; }
    }
}
