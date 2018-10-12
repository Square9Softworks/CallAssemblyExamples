using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Encompass2018.TwilioWrapper
{
    public class TwilioService
    {
        readonly string accountSid;
        readonly string authToken;
        readonly string accountPhoneNumber;
        public TwilioService()
        {
            accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            accountPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];
        }
        public void SendText(string message, string phoneNumber)
        {
            TwilioClient.Init(accountSid, authToken);

            var twilText = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(accountPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );
        }
    }
}
