using NotificationsManager.Interfaces;
using System.Collections.Generic;

namespace NotificationsManager.Classes
{
    public class NotificationBody
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}