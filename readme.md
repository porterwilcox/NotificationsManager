# NotificationsManager
### A class library for working with Azure Notification Hubs

## How to start
> `git clone` into desired server project
>
> `dotnet restore`
>
> in `Classes/` create a new file called `Config.cs`. Paste the code below.
```cs
namespace NotificationsManager.Config
{
    public class Settings
    {
        public static string AccessSignature = "<ReplaceMe>"
    }
}
```
> `<ReplaceMe>` with the DefaultFullSharedAccessSignature Connection String for the PortalNotifications *(CidiNotifications/PortalNotifications)* hub in the [Azure Portal](https://portal.azure.com/). <br/> *The signature is located in the* Access Policies *tab of the left side navigation bar when viewing the notification hub within the Azure Portal.*
>
> `dotnet build`
>
> You should be ready to instantiate and utilize a `NotificationManager` elsewhere in your server!

---
## Configure an iOS app