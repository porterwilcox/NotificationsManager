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
			var ApnsSimple = new InstallationTemplate();
			var ApnsFull = new InstallationTemplate();
			var MpnsFcmSimple = new InstallationTemplate();
			var MpnsFcmFull = new InstallationTemplate();

			ApnsSimple.Body = "{\"aps\":{\"alert\":\"$(message)\"}}";
			ApnsFull.Body = "{\"aps\":{\"alert\":{\"title\":\".(title, 50)\", \"subTitle\":\"$(subTitle)\", \"body\":\"$(message)\"}}";
			MpnsFcmSimple.Body = "{\"notification\":{\"body\":\"$(message)\"}}";
			MpnsFcmFull.Body = "{\"notification\":{\"title\":\".(title, 50)\", \"body\":\"$(message)\"}}";

			ApnsTemplates.Add("ApnsSimple", ApnsSimple);
			ApnsTemplates.Add("ApnsFull", ApnsFull);
			MpnsFcmTemplates.Add("MpnsFcmSimple", MpnsFcmSimple);
			MpnsFcmTemplates.Add("MpnsFcmFull", MpnsFcmFull);

		}
    }
}