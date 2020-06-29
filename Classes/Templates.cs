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
            ApnsFull.Body = "{\"aps\":{\"alert\":{\"title\":\".(title, 33)\", \"subTitle\":\"$(subTitle)\", \"body\":\"$(message)\"}}}";
            MpnsFcmSimple.Body = "<toast><visual><binding template=\"ToastText01\"><text id=\"1\">$(message)</text></binding></visual></toast>";
            MpnsFcmFull.Body = "<toast><visual><binding template=\"ToastText01\"><text id=\"1\"><b>.(title, 33)</b></text><text id=\"2\"><i>$(subTitle)</i></text><text id=\"3\">$(message)</text></binding></visual></toast>";

            ApnsTemplates.Add("ApnsSimple", ApnsSimple);
            ApnsTemplates.Add("ApnsFull", ApnsFull);
            MpnsFcmTemplates.Add("MpnsFcmSimple", MpnsFcmSimple);
            MpnsFcmTemplates.Add("MpnsFcmFull", MpnsFcmFull);

        }
    }
}