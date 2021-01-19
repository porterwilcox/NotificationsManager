using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using NotificationsManager.Interfaces;

namespace NotificationsManager.Classes
{
    public class Registration
    {
        private NotificationHubClient _hubClient;
        private Templates _templates = new Templates();
        public Registration(NotificationHubClient hubClient)
        {
            _hubClient = hubClient;
        }

        public async Task<string> CreateOrUpdateInstallation(IDeviceInstallation deviceData)
        {
            if(deviceData.Handle == null || deviceData.InstallationId == null || deviceData.Platform == null)
            {
                throw new Exception("[ERROR] Must provide a PNS Handle, an InstallationId, and a Platform.");
            }

            if(deviceData.Tags == null)
            {
                deviceData.Tags = new List<string>();
            }

            //NOTE Installation class docs -> https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.notificationhubs.installation?view=azure-dotnet
            Installation installation = new Installation();
            installation.InstallationId = deviceData.InstallationId;
            installation.PushChannel = deviceData.Handle;
            installation.Tags = deviceData.Tags;
            installation.Templates = new Dictionary<string, InstallationTemplate>();

            switch (deviceData.Platform = deviceData.Platform.ToLower())
            {
                case "fcm":
                case "android":
                    installation.Platform = NotificationPlatform.Fcm;
                    break;
                case "apns":
                case "ios":
                    installation.Platform = NotificationPlatform.Apns;
                    break;
                case "mpns":
                case "windows":
                    installation.Platform = NotificationPlatform.Mpns;
                    break;
                default:
                    throw new Exception("[ERROR] Device platform must be one of -> 'mpns' | 'windows' | 'apns' | 'ios' | 'fcm' | 'android'");
            }

            addTemplates(deviceData.Platform, installation.Templates);

            // In the backend we can control if a user is allowed to add tags
            //installation.Tags = new List<string>(deviceData.Tags);
            //installation.Tags.Add("username:" + username);

            try
            {
                await _hubClient.CreateOrUpdateInstallationAsync(installation);
                return "[SUCCESS] Device ready for push notifications!";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void addTemplates(string platform, IDictionary<string, InstallationTemplate> installationTemplates)
        {
            Dictionary<string, InstallationTemplate> templates = new Dictionary<string, InstallationTemplate>();
            switch(platform){
                case "apns":
                case "ios":
                    templates = _templates.ApnsTemplates;
                    break;
                case "fcm":
                case "android":
                case "mpns":
                case "windows":
                    templates = _templates.MpnsFcmTemplates;
                    break;
            }

            foreach(KeyValuePair<string, InstallationTemplate> kvp in templates)
            { //NOTE Break referrence to dictionary in Templates, just in case.
                installationTemplates.Add(kvp.Key, kvp.Value);
            }
        }

        public async Task<string> patchInstallationAsync(string installationId, IInstallationUpdate IInstallationUpdate)
        {
            if(IInstallationUpdate.TagToAdd == null && IInstallationUpdate.TagToRemove == null)
            {
                throw new Exception("You must specify (a) tag(s) to add, remove, or replace.");
            }

            PartialUpdateOperation tagsOpertation = new PartialUpdateOperation();

            if (IInstallationUpdate.TagToRemove == null)
            { //NOTE adding
                tagsOpertation.Operation = UpdateOperationType.Add;
                tagsOpertation.Path = "/tags";
            } else if (IInstallationUpdate.TagToAdd == null)
            { //NOTE removing
                tagsOpertation.Operation = UpdateOperationType.Remove;
                tagsOpertation.Path = "/tags/" + IInstallationUpdate.TagToRemove;
            } else
            { //NOTE replacing
                tagsOpertation.Operation = UpdateOperationType.Replace;
                tagsOpertation.Path = "/tags/" + IInstallationUpdate.TagToRemove;
                tagsOpertation.Value = IInstallationUpdate.TagToAdd;
            }
            
            try
            {
                await _hubClient.PatchInstallationAsync(installationId, new List<PartialUpdateOperation>(){tagsOpertation});
                return "Successfully updated the installation!";
            }
            catch (Exception e)
            {
            throw e;
            }
        }
    }
}