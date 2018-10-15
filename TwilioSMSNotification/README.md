# SMS Notifications using [Twilio](https://www.twilio.com/)

### A sample project that can be used by a Call Assembly node to send notifications to users via SMS messaging
[Twilio][1] provides a number of communication APIs for things like phone, SMS, MMS messaging etc. This project compiles into a .dll that can be utilized by Call Assembly to notify certain user(s) via SMS of something that has happened in a GA/GC process.



### Configuration
#### Twilio Account
First and foremost you will need an account with [Twilio][1]. Once you have created that, the following keys need to be added to `GlobalAction.exe.config` or `GlobalCapture.exe.config` depending on which product you are using. 
```
<add key="TwilioAccountSid" value="Your account SID from your twilio account homepage" />
<add key="TwilioAuthToken" value="Your account AuthToken from your twilio account homepage" />
<add key="TwilioPhoneNumber" value="The phone number (including country code) to send texts from that your twilio account owns." />
```

**IMPORTANT NOTE: This and all phone numbers in code and config files MUST include country code**

Update the values with the correct information from your Twilio account as denoted above. For more information, see [Twilio QuickStart guide for SMS with c#](https://www.twilio.com/docs/sms/quickstart/csharp-dotnet-framework)

#### TwilioConfigFile.json

Without modifying the source code, the following can be configured using a `.json ` file, this file will be loaded by default from `c:\GetSmart\TwilioConfigFile.json`
* `NotifyFields`: The ProcessField IDs in the workflow that will be checked to determine who to notify. When building your workflow, include one or more ProcessFields named "User to Notify". Multiple users can be notified by having multiple ProcessFields.
* `CustomMessage`: The message that will be sent to the User. 
* `Users`: The phonebook of users. In the sample code, the `NotifyFields` are iterated through, the ID in that field is looked up in this phonebook, and the phone number is sent an SMS message. If you added a ProcessField called "User to Notify", set that field to an ID in your phonebook before the CallAssembly node. 

The following sample configuration file is included in this repository. As you can see, there are two `NotifyFields` that are looking at ProcessFields with the IDs 2 and 3. If these fields contain an ID between 1 and 3, then SMS will be sent to that corresponding user's phone number in the phonebook.
```
{
  "NotifyFields": [
    2,
    3
  ],
  "CustomMessage": "There's a new document for you to review. Please log into GlobalSearch to take action.",
  "Users": {
    "1": {
      "ID": 1,
      "Name": "Pete",
      "PhoneNumber": "+12345678901"
    },
    "2": {
      "ID": 2,
      "Name": "Ron",
      "PhoneNumber": "+13456789012"
    },
    "3": {
      "ID": 3,
      "Name": "Jeff",
      "PhoneNumber": "+14567890123"
    }
  }
}
```

### Modification
#### `RunCallAssembly()` and `Input`
The key entry point as with all CallAssembly compatible .dlls is in `CATwilio.cs` via the `RunCallAssembly()` method.  

This method takes in a `Dictionary<string, string> Input` which represents the ProcessField tuples from the current process, as well as, in the case of GlobalAction, GlobalSearch metadata DatabaseID, ArchiveID, and DocumentID.  

These values can be parlayed into a direct link to GlobalSearch Web, sending additional information via SMS to the NotifyUser, or even additional integrations with other services. All code within the `RunCallAssembly()` method will be run by the engine.

[1]:https://www.twilio.com

