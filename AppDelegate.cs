using System.Drawing;
using Foundation;
using UIKit;
using Firebase.CloudMessaging;
using System;
using UserNotifications;


namespace nucaapp
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate, IMessagingDelegate, IUNUserNotificationCenterDelegate
    {
    
        [Export("window")]
        public UIWindow Window { get; set; }

        [Export ("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {
            Firebase.Core.App.Configure();
            // Firebase.Analytics.Analytics.SetAnalyticsCollectionEnabled(true);
            Messaging.SharedInstance.Delegate = this;

            UINavigationBar.Appearance.TintColor = UIColor.Clear;
            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(14, 14, 166);

        //    UIImage image1 = new UIImage("home");
         //   UITabBarItem uITabBarItem = new UITabBarItem("Spin", image1, 1);
         //   uITabBarItem.Image = UIImage.FromBundle("home");

            // Color of the tabbar background:
            UITabBar.Appearance.BarTintColor = UIColor.FromRGB(222, 222, 222);

            // Color of the selected tab text color:
            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes()
                {
                    
                    TextColor = UIColor.FromRGB(14, 14, 166)
                },
                UIControlState.Selected);

            // Color of the unselected tab icon & text:
            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes()
                {
                    TextColor = UIColor.FromRGB(0, 0, 0)
                },
                UIControlState.Normal);

            return true;
        }

        // UISceneSession Lifecycle

        [Export ("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration (UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options)
        {
            // Called when a new scene session is being created.
            // Use this method to select a configuration to create the new scene with.
            return UISceneConfiguration.Create ("Default Configuration", connectingSceneSession.Role);
        }

        [Export ("application:didDiscardSceneSessions:")]
        public void DidDiscardSceneSessions (UIApplication application, NSSet<UISceneSession> sceneSessions)
        {
            // Called when the user discards a scene session.
            // If any sessions were discarded while the application was not running, this will be called shortly after `FinishedLaunching`.
            // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
        }
        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage, string fcmToken)
        {
            Console.WriteLine(remoteMessage);
            Console.WriteLine($"Firebase registration token: {fcmToken}");
        }
        [Export("messaging:didReceiveMessage:")]
        public void DidReceiveMessage(Messaging messaging, RemoteMessage remoteMessage)
        {
        }

        public  void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
         
        }

        public  void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Console.WriteLine(userInfo);
            completionHandler(UIBackgroundFetchResult.NewData);
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);
            // Generate custom event
            NSString[] keys = { new NSString("Event_type") };
            NSObject[] values = { new NSString("Recieve_Notification") };
            var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);



            if (application.ApplicationState == UIApplicationState.Active)
            {
                System.Diagnostics.Debug.WriteLine(userInfo);
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSDictionary;
                var body = alert_d["body"] as NSString;
                var title = alert_d["title"] as NSString;
                debugAlert(title, body);
            }


        }


        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            // Print full message.
            Console.WriteLine(userInfo);
            completionHandler();
        }
        public  void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();


        }
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var userInfo = notification.Request.Content.UserInfo;
            var title = notification.Request.Content.Title;
            var body = notification.Request.Content.Body;
            debugAlert(title, body);
            // With swizzling disabled you must let Messaging know about the message, for Analytics
            //Messaging.SharedInstance.AppDidReceiveMessage (userInfo);

            // Print full message.
            Console.WriteLine(userInfo);

            // Change this to your preferred presentation option
            completionHandler(UNNotificationPresentationOptions.None);
        }

        private void debugAlert(string title, string message)
        {
            UIAlertView alert = new UIAlertView(title ?? "Title", message ?? "Message", null, "Cancel", "OK");
            alert.Show();
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            Console.WriteLine($"Firebase registration token: {fcmToken}");

            // TODO: If necessary send token to application server.
            // Note: This callback is fired at each app startup and whenever a new token is generated.
        }
    }
}


