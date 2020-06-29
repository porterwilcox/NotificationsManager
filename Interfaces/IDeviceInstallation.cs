using System.Collections.Generic;

namespace NotificationsManager.Interfaces
{
    public interface IDeviceInstallation
    {
        string InstallationId { get; set; } 
        string Handle { get; set; }
        string Platform { get; set; }
        List<string> Tags { get; set; }

    }

    public interface IInstallationUpdate
    {
        string TagToRemove { get; set; }

        string TagToAdd { get; set; }
    }
}