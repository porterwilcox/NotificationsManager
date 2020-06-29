using System.Collections.Generic;

namespace NotificationsManager.Classes
{
    public class Notification : Dictionary<string, string>
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}
