using System.Collections.Generic;

namespace NotificationsManager.Interfaces
{
    public interface INotificationBody
    {
        string Message { get; set; }
        string Title { get; set; }
        string Subtitle { get; set; }
    }
}