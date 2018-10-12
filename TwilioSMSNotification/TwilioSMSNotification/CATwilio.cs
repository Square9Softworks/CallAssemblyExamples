using System;
using System.Collections.Generic;

using Encompass2018.TwilioWrapper;

namespace Encompass2018
{
    public class CATwilio
    {
        /*
         * You must have the following entries in your GlobalAction.exe.config <AppSettings> section:
         * <add key="TwilioAccountSid" value="Your account SID from your twilio account homepage" />
         * <add key="TwilioAuthToken" value="Your account AuthToken from your twilio account homepage" />
         * <add key="TwilioPhoneNumber" value="The phone number (including country code) to send texts from that your twilio account owns." />
         *
         * */
        public Dictionary<string, string> RunCallAssembly(Dictionary<string, string> Input)
        {
            var twilio = new TwilioService();
            //TwilioConfigFile can take a path string as a parameter, 
            //if an empty or null string is passed it defaults to C:\Getsmart\TwilioConfigFile.json
            TwilioConfig notifyConfig = new TwilioConfigFile("").GetConfig();

            //iterate through the notify fields in our config file, and then check the processfield
            //for the ID or name to send a text to.
            foreach (var notifyProcessField in notifyConfig.NotifyFields)
            {
                //NotifyID is the ID of our ActionUser that we want to send our message to
                if (Input.TryGetValue(notifyProcessField.ToString(), out string NotifyUser))
                {
                    if (notifyConfig.Phonebook.TryGetValue(NotifyUser, out User user))
                    {
                        //e.g if notifyProcessField was 2, and Input["2"] contained "3", then we would send
                        //our message to the user in our config file with an ID of 3.
                        twilio.SendText(notifyConfig.CustomMessage, user.PhoneNumber);
                    }
                    else
                    {
                        //NotifyUser did not exist in our phonebook.
                    }
                }
                else
                {
                    //ProcessFields passed from GA do not contain this notify field ID.
                }
            }
            //Texts will have been sent, return our processfields, unmodified.
            return Input;
        }
    }
}
