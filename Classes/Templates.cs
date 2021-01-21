using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;

namespace NotificationsManager.Classes
{
    public class Templates
    {
        public Dictionary<string, InstallationTemplate> ApnsTemplates = new Dictionary<string, InstallationTemplate>();
        public Dictionary<string, InstallationTemplate> MpnsFcmTemplates = new Dictionary<string, InstallationTemplate>();

        public Templates()
        {
			//NOTE "Simple" templates not in use anymore but there for reference. 
			//Important: when adding mulitple templates, 
			//use different property variables for every template

			//var ApnsSimple = new InstallationTemplate();
			//var MpnsFcmSimple = new InstallationTemplate();
			var ApnsFull = new InstallationTemplate();
			var MpnsFcmFull = new InstallationTemplate();

			//https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-templates-cross-platform-push-messages#template-expression-language

			//ApnsSimple.Body = "{\"aps\":{\"alert\":\"$(message)\"}}";
			//MpnsFcmSimple.Body = "{\"notification\":{\"body\":\"$(message)\"}}";
			ApnsFull.Body = "{\"aps\":{\"alert\":{\"title\":\".(title, 50)\", \"subTitle\":\"$(subTitle)\", \"body\":\"$(message)\"}}";
			MpnsFcmFull.Body = "{\"notification\":{\"title\":\".(title, 50)\", \"body\":\"$(message)\"}, \"data\": {\"navTo\":\"$(navTo)\"}}";

			//ApnsTemplates.Add("ApnsSimple", ApnsSimple);
			//MpnsFcmTemplates.Add("MpnsFcmSimple", MpnsFcmSimple);
			ApnsTemplates.Add("ApnsFull", ApnsFull);
			MpnsFcmTemplates.Add("MpnsFcmFull", MpnsFcmFull);

		}
    }
}