using NotificationsManager.Interfaces;
using System.Collections.Generic;

namespace NotificationsManager.Classes
{
    public class DeviceInstallation : IDeviceInstallation
    {
        public string InstallationId { get; set; }
        public string Handle { get; set; }
        public string Platform { get; set; }
        public List<string> Tags { get; set; }
    }

    public class InstallationUpdate : IInstallationUpdate
    {
        public string TagToRemove { get; set; }

        public string TagToAdd { get; set; }
    }
}