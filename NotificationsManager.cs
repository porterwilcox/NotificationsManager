﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using NotificationsManager.Classes;
using NotificationsManager.Config;
using NotificationsManager.Interfaces;
using System.Collections.Generic;


namespace NotificationsManager
{
    ///<summary>
    ///Your class library for working with Azure Notification Hubs
    ///</summary>
    public class NotificationManager
    {
        private NotificationHubClient _hubClient;
        private Registration _registration;
        private MailMan _mailMan;

        ///<summary>   
        ///Your class library for working with Azure Notification Hubs
        ///</summary>
        public NotificationManager()
        {
            _hubClient = NotificationHubClient.CreateClientFromConnectionString(Settings.AccessSignature, "IGS-CustomerMobile");
            _registration = new Registration(_hubClient);
            _mailMan = new MailMan(_hubClient);
        }  

        ///<summary>
        ///Use this method to register a device. Creates a device installation asynchronously. Will not create multiple installations for same device if one already exists with provided deviceData. 
        ///</summary>
        ///<remarks>
        ///Learn about device installations here: https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-push-notification-registration-management#registration-management-from-a-backend
        ///</remarks>
        ///<returns>
        ///Success message string, else throws Exception.
        ///</returns>
        ///<param name="deviceData"> 
        ///A payload with the following properties:
        /// - InstallationId: string -> The unique string randomly generated and stored in the client that identifies the Installation.
        /// - Handle: string -> The unique device identifier (eg. device token or FCM registration ID) only retrievable from the clientside.
        /// - Platform: string ['mpns' | 'windows' | 'apns' | 'ios' | 'fcm' | 'android'] -> Client device OS or corresponding push platform acronym.
        /// - Tags: List[string] -> Adding tags to device installations allows for targeting devices by tag for push notifications. See more: https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-tags-segment-push-message
        ///</param>
        public async Task<string> Register(IDeviceInstallation deviceData)
        {
            try
            {
                return await _registration.CreateOrUpdateInstallation(deviceData);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///<summary>
        ///Use this method to update an installation's tags asynchronously. To REPLACE an existing tag with a new one, populate both properties (namely TagToRemove and TagToAdd) on the second argument. To ADD or REMOVE a tag, on the second argument populate the corresponding property appropriately and set the other to null.
        ///</summary>
        ///<remarks>
        ///Learn about tags here: https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-tags-segment-push-message
        ///</remarks>
        ///<returns>
        ///Success message string, else throws Exception.
        ///</returns>
        ///<param name="installationId">
        ///The unique string stored in the client that identifies the Installation.
        ///</param>
        ///<param name="installationUpdate"> 
        ///A payload with the following properties:
        /// - TagToRemove: string | null
        /// - TagToAdd: string | null
        ///</param>
        public async Task<string> UpdateInstallationTags(string installationId, IInstallationUpdate installationUpdate)
        {
            try
            {
                return await _registration.patchInstallationAsync(installationId, installationUpdate);
            }
            catch (Exception e)
            {
            throw e;
            }
        }

        ///<summary>
        ///Use this method to get an Installation by the InstallationId.
        ///</summary>
        ///<returns>
        ///An Installation instance
        ///</returns>
        ///<param name="installationId">
        ///The unique string stored in the client that identifies the Installation.
        ///</param>
        public Task<Installation> GetInstallation(string installationId)
        {
            try
            {
                return _registration.GetInstallation(installationId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///<summary>
        ///Use this method to delete an Installation by the InstallationId.
        ///</summary>
        ///<returns>
        ///Void
        ///</returns>
        ///<param name="installationId">
        ///The unique string stored in the client that identifies the Installation.
        ///</param>
        public Task DeleteInstallation(string installationId)
        {
            try
            {
                return _registration.DeleteInstallation(installationId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///<summary>
        ///Use this method to send a push notification to all devices registered to the Notification Hub.
        ///</summary>
        ///<returns>
        ///Success notification outcome, else throws Exception.
        ///</returns>
        ///<param name="notification">
        ///A payload with the following properties:
        ///Message: string
        ///?SubTitle: string
        ///?Title: string
        ///</param>
        public async Task<NotificationOutcome> NotifyAll(NotificationBody notification)
        {
            try
            {
                return await _mailMan.NotifyAll(notification);
            }
            catch (Exception e)
            {
            throw e;
            }
        }
        
        ///<summary>
        ///Use this method to send a push notification to all devices registered to the Notification Hub that have at least one or more of the corresponding tags.
        ///</summary>
        ///<returns>
        ///Success notification outcome, else throws Exception.
        ///</returns>
        ///<param name="notification">
        ///A payload with the following properties:
        ///Message: string
        ///?SubTitle: string
        ///?Title: string
        ///</param>
        public async Task<NotificationOutcome> NotifyByHasTags(NotificationBody notification)
        {
            try
            {
                return await _mailMan.NotifyByHasTags(notification);
            }
            catch (Exception e)
            {
            throw e;
            }
        }
        
        ///<summary>
        ///Use this method to send a push notification to all devices registered to the Notification Hub that match the tag expression exactly.
        ///</summary>
        ///<returns>
        ///Success notification outcome, else throws Exception.
        ///</returns>
        ///<param name="notification">
        ///A payload with the following properties:
        ///Message: string
        ///?SubTitle: string
        ///?Title: string
        ///</param>
        ///<param name="tagExpression">
        ///A string of type tag expression. A tag expression is any boolean expression constructed using the logical operators AND (double ampersand), OR (||), NOT (!), and round parentheses. For example: (A || B) ampersandampersand !C. If an expression uses only ORs, it can contain at most 20 tags. Other expressions are limited to 6 tags. Note that a single tag "A" is a valid expression.
        ///</param>
        public async Task<NotificationOutcome> NotifyBySatisfysTagExpression(NotificationBody notification)
        {
            try
            {
                return await _mailMan.NotifyBySatisfysTagExpression(notification);
            }
            catch (Exception e)
            {
            throw e;
            }
        }

    }
}
