using Microsoft.Azure.NotificationHubs;
using NotificationsManager.Interfaces;
using System.Collections.Generic;

namespace NotificationsManager.Classes
{
    public class NotificationBody
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string NavTo { get; set; }
        public List<string> Tags { get; set; }
        public string TagExpression { get; set; }
	}
}