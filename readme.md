# NotificationsManager
### A class library for working with Azure Notification Hubs

## How to start
`git clone` into desired server project

`dotnet restore`

in `Classes/` create a new file called `Config.cs`. Paste the code below.
```cs
namespace NotificationsManager.Config
{
    public class Settings
    {
        public static string AccessSignature = "<ReplaceMe>"
    }
}
```
`<ReplaceMe>` with the DefaultFullSharedAccessSignature Connection String for the PortalNotifications *(CidiNotifications/PortalNotifications)* hub in the [Azure Portal](https://portal.azure.com/). <br/> 
>*The signature is located in the* Access Policies *tab of the left side navigation bar when viewing the notification hub within the Azure Portal.*

`dotnet build`

You should be ready to instantiate and utilize a `NotificationManager` elsewhere in your server!

<br/>

---
## NotificationManager docs

### available methods
*all methods are asynchronous and return Task< T >*


`.Register`

Use this method to register a device. Creates a device installation asynchronously. Will not create multiple installations for same device if one already exists with provided deviceData. 
>params: [IIDeviceInstallation](https://github.com/porterwilcox/NotificationsManager#IIDeviceInstallation) deviceData <br/>
>returns: string 

<br/>

`.UpdateInstallationTags`

Use this method to update an installation's tags asynchronously. To REPLACE an existing tag with a new one, populate both properties (namely TagToRemove and TagToAdd) on the second argument. To ADD or REMOVE a tag, on the second argument populate the corresponding property appropriately and set the other to null.
>params: string installationId, [IInstallationUpdate](https://github.com/porterwilcox/NotificationsManager#IInstallationUpdate) IInstallationUpdate <br/>
>returns: string 

<br/>

`.NotifyAll`

Use this method to send a push notification to all devices registered to the Notification Hub. 
>params: [NotificationBody](https://github.com/porterwilcox/NotificationsManager#NotificationBody) body <br/>
>returns: [NotificationOutcome](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.notificationhubs.notificationoutcome?view=azure-dotnet) 

<br/>

`.NotifyByHasTags`

Use this method to send a push notification to all devices registered to the Notification Hub that have at least one or more of the corresponding tags.
>params: [NotificationBody](https://github.com/porterwilcox/NotificationsManager#NotificationBody) body, string[] tags <br/>
>returns: [NotificationOutcome](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.notificationhubs.notificationoutcome?view=azure-dotnet) 

<br/>

`.NotifyBySatisfysTagExpression`

Use this method to send a push notification to all devices registered to the Notification Hub that match the tag expression exactly.
>params: [NotificationBody](https://github.com/porterwilcox/NotificationsManager#NotificationBody) body, string tagExpression <br/>
>returns: [NotificationOutcome](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.notificationhubs.notificationoutcome?view=azure-dotnet) 

<br/>

### expected payloads

##### `IIDeviceInstallation`
```typescript
string InstallationId  
string Handle 
string Platform ['mpns' | 'windows' | 'apns' | 'ios' | 'fcm' | 'android']
List<string> Tags 
```
>InstallationId: The unique string randomly generated and stored in the client that identifies the Installation. <br/><br/>
Handle: The unique device identifier (eg. device token or FCM registration ID) only retrievable from the clientside. <br/><br/>
Platform: Client device OS or corresponding push platform acronym. <br/><br/>
Tags: Adding tags to device installations allows for targeting devices by tag for push notifications. [learn more](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-tags-segment-push-message#tags)

<br/>

##### `IInstallationUpdate`
```typescript
string TagToRemove
string TagToAdd
```
>To REPLACE an existing tag with a new one, populate both properties on the second argument. To ADD or REMOVE a tag, populate the corresponding property appropriately and set the other to null.

<br/>

##### `NotificationBody`
```typescript
string Message
string Title
string Subtitle
```
>message is required <br/>if only message is supplied then a simple notification will be sent, else notification with title and optional sub-title

<br/>

[learn more](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-tags-segment-push-message#tags) about tags <br/> or <br/> [learn more](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-tags-segment-push-message#tag-expressions) about tag expressions

<br/>

---
## Front end configuration
 `npm i nativescript-plugin-firebase` <br/>
>Download will prompt multiple questions. Say no to all except these three: <br/>*Are you using iOS (y/n)<br/>Are you using Android (y/n)<br/>Are you using this plugin as a Push Notification client for an external (NOT Firebase Cloud Messaging) Push service? (y/n)*

follow the quick config steps for each platform found on the plugin's [readme](https://github.com/EddyVerbruggen/nativescript-plugin-firebase/blob/master/docs/NON_FIREBASE_MESSAGING.md#setup)

inside of `main.ts` include this
```typescript
// NOTE without this, you won't receive a push token on iOS
require("nativescript-plugin-firebase");
```

You should be ready to utilize `messaging` within your project! 
```typescript 
import { messaging } from "nativescript-plugin-firebase/messaging";

messaging.registerForPushNotifications({
            onPushTokenReceivedCallback: (token: string): void => {
                //NOTE remember we're not using this plugin as our push notification manager 
                //(i.e., our device isn't registered with Azure Notification Hubs yet)               
                
                //TODO send the device token to the backend
            }
        })
        .then(() => console.log("device confirmed push notifications!"))
```

<br/>

---
## Configure the Cidi/Portal Notification Hub to the final productions

follow the microsoft documentation found [here](https://docs.microsoft.com/en-us/azure/notification-hubs/configure-notification-hub-portal-pns-settings?tabs=azure-portal)