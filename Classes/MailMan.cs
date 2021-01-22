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
                    { "message", body.Message }
                };
                if (body.Title != null) dict.Add("title", body.Title);
                if (body.Subtitle != null) dict.Add("subTitle", body.Subtitle);
                if (body.NavTo != null) dict.Add("navTo", body.NavTo);
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
        
        public async Task<NotificationOutcome> NotifyAll(NotificationBody notification)
        {
            try
            {
                IsValidNotificaitonBody(notification);
                return await _hubClient.SendNotificationAsync(BuildNotification(notification));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NotificationOutcome> NotifyByHasTags(NotificationBody notification)
        {
            try
            {
                var tags = notification.Tags;
                if(tags == null || tags.Count() == 0)
                {
                    throw new Exception("Must provide tags to target. Please provide tags, else use NotifyAll.");
                }
                IsValidNotificaitonBody(notification);
				return await _hubClient.SendNotificationAsync(BuildNotification(notification), tags);
			}
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NotificationOutcome> NotifyBySatisfysTagExpression(NotificationBody notification)
        {
            try
            {
                var tagExpression = notification.TagExpression;
                if(tagExpression == null || tagExpression.Length == 0)
                {
                    throw new Exception("Must provide a valid tag expression. Please provide a tag expression, else us NotifyAll.");
                }
                IsValidNotificaitonBody(notification);
                return await _hubClient.SendNotificationAsync(BuildNotification(notification), tagExpression);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}