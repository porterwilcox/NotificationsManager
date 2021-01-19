using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using NotificationsManager.Interfaces;

namespace NotificationsManager.Classes
{
    public class MailMan
    {
        private NotificationHubClient _hubClient;
        public MailMan(NotificationHubClient hubClient)
        {
            _hubClient = hubClient;
        }

        private void IsValidNotificaitonBody(NotificationBody body)
        {
            if(body.Message == null)
            {
                throw new Exception("Must provide a valid notification payload. [Error]: Missing property Message: string");
            }
        }

        private TemplateNotification BuildNotification(NotificationBody body)
        {
            try
            {
                var dict = new Dictionary<string, string> { 
                    { "Message", body.Message },
                    { "Title", body.Title ?? "" },
                    { "Subtitle", body.Subtitle ?? "" } 
                };
                TemplateNotification notification = new TemplateNotification(dict);
                //NOTE As of iOS 13 apple requires these headers for push notifications. (as opposed to 'background')
                notification.Headers = new Dictionary<string, string> {{"apns-push-type", "alert"}};
                return notification;
            }
            catch (Exception)
            {
                throw new Exception("Could not successfully construct a TemplateNotification from payload.");
            }
        }
        
        public async Task<NotificationOutcome> NotifyAll(NotificationBody body)
        {
            try
            {
                IsValidNotificaitonBody(body);
                return await _hubClient.SendNotificationAsync(BuildNotification(body));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NotificationOutcome> NotifyByHasTags(NotificationBody body, IEnumerable<string> tags)
        {
            try
            {
                if(tags == null || tags.Count() == 0)
                {
                    throw new Exception("Must provide tags to target. Please provide tags, else use NotifyAll.");
                }
                IsValidNotificaitonBody(body);
                return await _hubClient.SendNotificationAsync(BuildNotification(body), tags);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NotificationOutcome> NotifyBySatisfysTagExpression(NotificationBody body, string tagExpression)
        {
            try
            {
                if(tagExpression == null || tagExpression.Length == 0)
                {
                    throw new Exception("Must provide a valid tag expression. Please provide a tag expression, else us NotifyAll.");
                }
                IsValidNotificaitonBody(body);
                return await _hubClient.SendNotificationAsync(BuildNotification(body), tagExpression);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}