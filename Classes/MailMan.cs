using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace NotificationsManager.Classes
{
    public class MailMan
    {
        private NotificationHubClient _hubClient;
        public MailMan(NotificationHubClient hubClient)
        {
            _hubClient = hubClient;
        }

        private void IsNotification(Notification notification)
        {
            if(!notification.ContainsKey("Message") || notification.Message == null)
            {
                throw new Exception("Must provide a valid notification payload.");
            }
        }
        
        public async Task<NotificationOutcome> NotifyAll(Notification notification)
        {
            try
            {
                IsNotification(notification);
                return await _hubClient.SendTemplateNotificationAsync(notification);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NotificationOutcome> NotifyByHasTags(Notification notification, IEnumerable<string> tags)
        {
            try
            {
                IsNotification(notification);
                if(tags.Count() == 0)
                {
                    throw new Exception("Must provide tags to target. Please provide tags else use NotifyAll.");
                }
                return await _hubClient.SendTemplateNotificationAsync(notification, tags);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NotificationOutcome> NotifyBySatisfysTagExpression(Notification notification, string tagExpression)
        {
            try
            {
                IsNotification(notification);
                return await _hubClient.SendTemplateNotificationAsync(notification, tagExpression);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}