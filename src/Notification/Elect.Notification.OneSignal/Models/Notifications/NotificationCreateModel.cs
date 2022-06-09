#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationCreateOptions.cs </Name>
//         <Created> 19/03/2018 9:44:00 PM </Created>
//         <Key> 4ec83ea8-886b-4e40-a35f-32a6062831a2 </Key>
//     </File>
//     <Summary>
//         NotificationCreateOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.JsonConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     API Documentation: https://documentation.onesignal.com/docs/notifications-create-notification 
    /// </summary>
    public class NotificationCreateModel
    {
        /// <summary>
        ///     Default constructor that initializes empty Contents and Headings. All other
        ///     collection objects needs to be instantiated in order to be able to be serialized.
        /// </summary>
        public NotificationCreateModel()
        {
        }

        /// <summary>
        ///     <br /> Your OneSignal application ID, which can be found on our dashboard at
        ///     onesignal.com under App Settings &gt; Keys &amp; IDs. <br /> It is a UUID and looks
        ///     similar to 8250eaf6-1a58-489e-b136-7c74a864b434. <br />
        /// </summary>
        [JsonProperty("app_id")]
        internal string AppId { get; set; }

        /// <summary>
        ///     <br /> The notification's content (excluding the title), a map of language codes to
        ///     text for each language. <br /> Each hash must have a language code string for a key,
        ///     mapped to the localized text you would like users to receive for that language.
        ///     <br /> English must be included in the hash. <br />
        ///     <code>
        /// Example: {"en": "English Message", "es": "Spanish Message"}
        ///     </code>
        ///     <br /> See the language codes you can use
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here
        ///     </see> . <br />
        /// </summary>
        [JsonProperty("contents")]
        public IDictionary<string, string> Contents { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     <br /> The notification's title, a map of language codes to text for each language.
        ///     <br /> Each hash must have a language code string for a key, mapped to the localized
        ///     text you would like users to receive for that language. <br /> A default title may be
        ///     displayed if a title is not provided. <br />
        ///     <code>
        /// Example: {"en": "English Title", "es": "Spanish Title"}
        ///     </code>
        ///     <br /> See the language codes you can use
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here
        ///     </see> . <br />
        /// </summary>
        [JsonProperty("headings")]
        public IDictionary<string, string> Headings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     <br /> A custom map of data that is passed back to your app. <br /> 
        ///     <code>
        /// Example: {"abc": "123", "foo": "bar"}
        ///     </code>
        ///     <br /> See the language codes you can use
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here
        ///     </see> . <br />
        /// </summary>
        [JsonProperty("data")]
        public IDictionary<string, string> Data { get; set; }

        /// <summary>
        ///     <br /> Targets notification recipients with filters. <br /> This is a array of JSON
        ///     objects containing field conditions to check. <br />
        /// </summary>
        [JsonProperty("filters")]
        public IList<INotificationFilter> Filters { get; set; }

        /// <summary>
        ///     <br /> Send based on OneSignal PlayerIds <br /> 
        /// </summary>
        [JsonProperty("include_player_ids")]
        public IList<string> IncludePlayerIds { get; set; }
        
        [JsonProperty("include_external_user_ids")]
        public IList<string> IncludeUserIds { get; set; }

        /// <summary>
        ///     "push" or "email" or "sms"
        /// </summary>
        [JsonProperty("channel_for_external_user_ids")]
        public string ChannelForExternalUserIds { get; set; } = "push";

        /// <summary>
        ///     <br /> The segment names you want to target. <br /> Users in these segments will
        ///     receive a notification. <br /> This targeting parameter is only compatible with
        ///     excluded_segments. <br />
        /// </summary>
        [JsonProperty("included_segments")]
        public IList<string> IncludedSegments { get; set; }

        /// <summary>
        ///     <br /> Sets the web push notification's icon. <br /> An image URL linking to a valid
        ///     image. <br /> Common image types are supported; GIF will not animate. <br /> We
        ///     recommend 256x256 (at least 80x80) to display well on high DPI devices. <br />
        ///     Firefox will also use this icon, unless you specify firefox_icon. <br />
        /// </summary>
        [JsonProperty("chrome_web_icon")]
        public string ChromeWebIcon { get; set; }

        /// <summary>
        ///     <br /> The notification's subtitle, a map of language codes to text for each
        ///     language. <br /> Each hash must have a language code string for a key, mapped to the
        ///     localized text you would like users to receive for that language. <br /> A default
        ///     title may be displayed if a title is not provided. <br /> This field supports
        ///     <see cref="!:https://documentation.onesignal.com/docs/notification-content#section-notification-content-substitution">
        ///     inline substitutions </see> . <br />
        ///     <code>
        /// Example: {"en": "English Subtitle", "es": "Spanish Subtitle"}
        ///     </code>
        ///     <br />
        ///     Platforms: iOS 10+ <br />
        /// </summary>
        [JsonProperty("subtitle")]
        public IDictionary<string, string> Subtitle { get; set; }

        /// <summary>
        ///     <br /> Use a template you setup on our dashboard. You can override the template
        ///     values by sending other parameters with the request. <br /> The template_id is the
        ///     UUID found in the URL when viewing a template on our dashboard. <br />
        ///     Platforms: ALL <br />
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        ///     <br /> Sending true wakes your app to run custom native code (Apple interprets this
        ///     as content-available=1). <br /> Omit contents field to make notification silent. <br />
        ///     Platforms: iOS <br />
        /// </summary>
        [JsonProperty("content_available")]
        public bool? ContentAvailable { get; set; }

        /// <summary>
        ///     <br /> Sending true allows you to change the notification content in your app before
        ///     it is displayed. <br /> Triggers didReceive(_:withContentHandler:) on your
        ///     UNNotificationServiceExtension. <br />
        ///     Platforms: iOS 10+ <br />
        /// </summary>
        [JsonProperty("mutable_content")]
        public bool? MutableContent { get; set; }

        /// <summary>
        ///     <br /> The URL to open in the browser when a user clicks on the notification. <br /> 
        ///     <code>
        /// Example: http://www.google.com
        ///     </code>
        ///     <br /> This field supports
        ///     <see cref="!:https://documentation.onesignal.com/docs/notification-content#section-notification-content-substitution">
        ///     inline substitutions </see> . <br />
        ///     Platforms: ALL <br />
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     <br /> Adds media attachments to notifications. Set as JSON object, key as a media id
        ///     of your choice and the value as a valid local file name or URL. <br /> User must
        ///     press and hold on the notification to view. <br /> Do not set mutable_content to
        ///     download attachments. The OneSignal SDK does this automatically. <br />
        ///     <code>
        /// Example: {"id1": "https://domain.com/image.jpg"}
        ///     </code>
        ///     <br />
        ///     Platforms: iOS 10+ <br />
        /// </summary>
        [JsonProperty("ios_attachments")]
        public IDictionary<string, string> IosAttachments { get; set; }

        /// <summary>
        ///     <br /> Picture to display in the expanded view. Can be a drawable resource name or a
        ///     URL. <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("big_picture")]
        public string BigPictureForAndroid { get; set; }

        /// <summary>
        ///     <br /> Picture to display in the expanded view. Can be a drawable resource name or a
        ///     URL. <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("adm_big_picture")]
        public string BigPictureForAmazon { get; set; }

        /// <summary>
        ///     <br /> Picture to display in the expanded view. Can be a drawable resource name or a
        ///     URL. <br />
        ///     Platforms: Chrome <br />
        /// </summary>
        [JsonProperty("chrome_big_picture")]
        public string BigPictureForChrome { get; set; }

        /// <summary>
        ///     <br /> Buttons to add to the notification. Icon only works for Android. <br /> 
        ///     <code>
        /// Example: [{"id": "id1", "text": "button1", "icon": "ic_menu_share"}, {"id": "id2", "text": "button2", "icon": "ic_menu_send"}]
        ///     </code>
        ///     <br />
        ///     Platforms: iOS 8.0+, Android 4.1+ (and derivatives like Amazon) <br />
        /// </summary>
        [JsonProperty("buttons")]
        public IList<ActionButtonFieldModel> ActionButtons { get; set; }

        /// <summary>
        ///     <br /> Add action buttons to the notification. The id field is required. <br /> 
        ///     <code>
        /// Example: [{"id": "like-button", "text": "Like", "icon": "http://i.imgur.com/N8SN8ZS.png", "url": "https://yoursite.com"}, {"id": "read-more-button", "text": "Read more", "icon": "http://i.imgur.com/MIxJp1L.png", "url": "https://yoursite.com"}]
        ///     </code>
        ///     <br />
        ///     Platforms: Chrome 48+ <br />
        /// </summary>
        [JsonProperty("web_buttons")]
        public IList<WebButtonFieldModel> WebButtons { get; set; }

        /// <summary>
        ///     <br /> Category APS payload, use with registerUserNotificationSettings:categories in
        ///     your Objective-C / Swift code. <br />
        ///     <code>
        /// Example: calendar category which contains actions like accept and decline
        ///     </code>
        ///     <br /> iOS 10+ This will trigger your UNNotificationContentExtension whose ID matches
        ///     this category. <br />
        ///     Platforms: iOS <br />
        /// </summary>
        [JsonProperty("ios_category")]
        public string IosCategory { get; set; }

        /// <summary>
        ///     <br /> Allowing setting a background image for the notification. This is a JSON
        ///     object containing the following keys. <br /> See our Background Image documentation
        ///     for
        ///     <see cref="!:https://documentation.onesignal.com/docs/android-customizations#section-background-images">
        ///     image sizes </see> . <br /> image - Asset file, android resource name, or URL to
        ///     remote image. <br /> headings_color - Title text color ARGB Hex format.
        ///     <code>
        /// Example(Blue): "FF0000FF".
        ///     </code>
        ///     <br /> contents_color - Body text color ARGB Hex format. 
        ///     <code>
        /// Example(Red): "FFFF0000"
        ///     </code>
        ///     <br /> 
        ///     <code>
        /// Example: {"image": "https://domain.com/background_image.jpg", "headings_color": "FFFF0000", "contents_color": "FF00FF00"}
        ///     </code>
        ///     <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("android_background_layout")]
        public IList<AndroidBackgroundLayoutFieldModel> AndroidBackgroundLayout { get; set; }

        /// <summary>
        ///     <br /> If blank the app icon is used. Must be the drawable resource name. <br /> See
        ///     how to create small icons: <see cref="!:https://documentation.onesignal.com/docs/android-customizations#section-small-notification-icons" /><br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("small_icon")]
        public string SmallAndroidIcon { get; set; }

        /// <summary>
        ///     <br /> If blank the small_icon is used. Can be a drawable resource name or a URL.
        ///     <br /> See how to
        ///     <see cref="!:https://documentation.onesignal.com/docs/android-customizations#section-large-notification-icons">
        ///     create large icons </see> . <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("large_icon")]
        public string LargeAndroidIcon { get; set; }

        /// <summary>
        ///     <br /> Specific Amazon icon to use. <br /> If blank the app icon is used. <br /> Must
        ///     be the drawable resource name. <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("adm_small_icon")]
        public string SmallAmazonIcon { get; set; }

        /// <summary>
        ///     <br /> Specific Amazon icon to display to the left of the notification. <br /> If
        ///     blank the adm_small_icon is used. <br /> Can be a drawable resource name or a URL. <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("adm_large_icon")]
        public string LargeAmazonIcon { get; set; }

        /// <summary>
        ///     <br /> This flag is not used for web push For web push, please see chrome_web_icon
        ///     instead. <br /> The local URL to an icon to use. If blank, the app icon will be used. <br />
        ///     Platforms: ChromeApp <br />
        /// </summary>
        [JsonProperty("chrome_icon")]
        public string ChromeIcon { get; set; }

        /// <summary>
        ///     <br /> Sound file that is included in your app to play instead of the default device
        ///     notification sound. <br /> Pass "nil" to disable vibration and sound for the
        ///     notification. <br />
        ///     Platforms: iOS <br />
        /// </summary>
        [JsonProperty("ios_sound")]
        public string IosSound { get; set; }

        /// <summary>
        ///     <br /> Sound file that is included in your app to play instead of the default device
        ///     notification sound. <br />
        ///     NOTE: Leave off file extension for Android. <br />
        ///     <code>
        /// Example: "notification"
        ///     </code>
        ///     <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("android_sound")]
        public string AndroidSound { get; set; }

        /// <summary>
        ///     <br /> Sound file that is included in your app to play instead of the default device
        ///     notification sound. <br />
        ///     NOTE: Leave off file extension for Android. <br />
        ///     <code>
        /// Example: "notification"
        ///     </code>
        ///     <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("adm_sound")]
        public string AmazonSound { get; set; }

        /// <summary>
        ///     <br /> Sound file that is included in your app to play instead of the default device
        ///     notification sound. <br />
        ///     <code>
        /// Example: "notification.wav"
        ///     </code>
        ///     <br />
        ///     Platforms: Windows 8.0 <br />
        /// </summary>
        [JsonProperty("wp_sound")]
        public string WindowsPhoneSound { get; set; }

        /// <summary>
        ///     <br /> Sound file that is included in your app to play instead of the default device
        ///     notification sound. <br />
        ///     <code>
        /// Example: "notification.wav"
        ///     </code>
        ///     <br />
        ///     Platforms: Windows 8.1+ <br />
        /// </summary>
        [JsonProperty("wp_wns_sound")]
        public string WindowsRtPhoneSound { get; set; }

        /// <summary>
        ///     <br /> Sets the devices LED notification light if the device has one. ARGB Hex
        ///     format. <br />
        ///     <code>
        /// Example(Blue): "FF0000FF"
        ///     </code>
        ///     <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("android_led_color")]
        public string AndroidLedColor { get; set; }

        /// <summary>
        ///     <br /> Sets the background color of the notification circle to the left of the
        ///     notification text. <br /> Only applies to apps targeting Android API level 21+ on
        ///     Android 5.0+ devices. <br />
        ///     <code>
        /// Example(Red): "FFFF0000"
        ///     </code>
        ///     <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("android_accent_color")]
        public string AndroidAccentColor { get; set; }

        /// <summary>
        ///     <br /> Sets the lock screen visibility for apps targeting Android API level 21+
        ///     running on Android 5.0+ devices. <br /> 1 = Public (default) (Shows the full message
        ///     on the lock screen unless the user has disabled all notifications from showing on the
        ///     lock screen. Please consider the user and mark private if the contents are.) <br /> 0
        ///     = Private (Hides message contents on lock screen if the user set "Hide sensitive
        ///       notification content" in the system settings) <br />
        ///     -1 = Secret (Notification does not show on the lock screen at all) <br />
        ///     Platforms: Android 5.0+ <br />
        /// </summary>
        [JsonProperty("android_visibility")]
        public AndroidVisibilityEnum? AndroidVisibility { get; set; }

        /// <summary>
        ///     <br /> Describes whether to set or increase/decrease your app's iOS badge count by
        ///     the ios_badgeCount specified count. Can specify None, SetTo, or Increase. <br /> None
        ///     - leaves the count unaffected. <br /> SetTo - directly sets the badge count to the
        ///       number specified in ios_badgeCount. <br /> Increase - adds the number specified in
        ///       ios_badgeCount to the total. Use a negative number to decrease the badge count. <br />
        ///     Platforms: iOS <br />
        /// </summary>
        [JsonProperty("ios_badgeType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public IosBadgeTypeEnum? IosBadgeType { get; set; }

        /// <summary>
        ///     <br /> Used with ios_badgeType, describes the value to set or amount to
        ///     increase/decrease your app's iOS badge count by. <br /> You can use a negative number
        ///     to decrease the badge count when used with an ios_badgeType of Increase. <br />
        ///     Platforms: iOS <br />
        /// </summary>
        [JsonProperty("ios_badgeCount")]
        public int? IosBadgeCount { get; set; }

        /// <summary>
        ///     <br /> Only one notification with the same id will be shown on the device. Use the
        ///     same id to update an existing notification instead of showing a new one. <br /> his
        ///     is known as apns-collapse-id on iOS and collapse_key on Android. <br />
        ///     Platforms: iOS 10+, Android <br />
        /// </summary>
        [JsonProperty("collapse_id")]
        public string CollapseId { get; set; }

        /// <summary>
        ///     <br /> Schedule notification for future delivery. <br /> Eventhough API suggests
        ///     diffent datetime formats, we are using following format: "2015-09-24 14:00:00
        ///     GMT-0700" <br />
        ///     Platforms: ALL <br />
        /// </summary>
        [JsonProperty("send_after")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? SendAfter { get; set; }

        /// <summary>
        ///     <br /> Possible values are: <br /> timezone (Deliver at a specific time-of-day in
        ///     each users own timezone) <br /> last-active (Deliver at the same time of day as each
        ///     user last used your app). <br /> If send_after is used, this takes effect after the
        ///     send_after time has elapsed. <br />
        ///     Platforms: ALL <br />
        /// </summary>
        [JsonProperty("delayed_option")]
        [JsonConverter(typeof(DelayedOptionJsonConverter))]
        public DelayedOptionEnum? DelayedOption { get; set; }

        /// <summary>
        ///     <br /> Use with delayed_option=timezone. <br /> 
        ///     <code>
        /// Example: "9:00AM"
        ///     </code>
        ///     <br />
        ///     Platforms: ALL <br />
        /// </summary>
        [JsonProperty("delivery_time_of_day")]
        public string DeliveryTimeOfDay { get; set; }

        /// <summary>
        ///     <br /> Time To Live - In seconds. <br /> The notification will be expired if the
        ///     device does not come back online within this time. <br /> The default is 259,200
        ///     seconds (3 days). <br />
        ///     Platforms: iOS, Android, Chrome, ChromeWeb <br />
        /// </summary>
        [JsonProperty("ttl")]
        public int? TimeToLive { get; set; }

        /// <summary>
        ///     <br /> Delivery priority through the push server (example GCM/FCM). 
        ///     <code>
        /// Pass 10 for high priority.
        ///     </code>
        ///     <br /> Defaults to normal priority for Android and high for iOS. <br /> For Android
        ///     6.0+ devices setting priority to high will wake the device out of doze mode. <br />
        ///     Platforms: iOS, Android, Chrome, ChromeWeb <br />
        /// </summary>
        [JsonProperty("priority")]
        public int? Priority { get; set; }

        /// <summary>
        ///     <br /> All notifications with the same group will be stacked together using Android's
        ///     Notification Stacking feature. <br /> More info
        ///     <see cref="!:https://developer.android.com/training/wearables/notifications/stacks.html">
        ///     here </see> . <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("android_group")]
        public string AndroidGroup { get; set; }

        /// <summary>
        ///     <br /> Summary message to display when 2+ notifications are stacked together. Default
        ///     is "# new messages". <br /> Include $[notif_count] in your message and it will be
        ///     replaced with the current number. <br /> Languages - The value of each key is the
        ///     message that will be sent to users for that language. "en" (English) is required.
        ///     <br /> The key of each hash is either a a 2 character language code or one of
        ///     zh-Hans/zh-Hant for Simplified or Traditional Chinese. <br /> Read about
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization#section-supported-languages">
        ///     supported languages </see> . <br />
        ///     <code>
        /// Example: {"en": "You have $[notif_count] new messages"}
        ///     </code>
        ///     <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("android_group_message")]
        public string AndroidGroupMessage { get; set; }

        /// <summary>
        ///     <br /> All notifications with the same group will be stacked together using Android's
        ///     Notification Stacking feature. <br /> More info
        ///     <see cref="!:https://developer.android.com/training/wearables/notifications/stacks.html">
        ///     here </see> . <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("adm_group")]
        public string AmazonGroup { get; set; }

        /// <summary>
        ///     <br /> Summary message to display when 2+ notifications are stacked together. <br />
        ///     Default is "# new messages". Include $[notif_count] in your message and it will be
        ///     replaced with the current number. <br /> "en" (English) is required. <br /> The key
        ///     of each hash is either a a 2 character language code or one of zh-Hans/zh-Hant for
        ///     Simplified or Traditional Chinese. <br /> The value of each key is the message that
        ///     will be sent to users for that language. <br />
        ///     <code>
        /// Example: {"en": "You have $[notif_count] new messages"}
        ///     </code>
        ///     <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("adm_group_message")]
        public string AmazonGroupMessage { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all devices registered under your app's Apple iOS
        ///     platform. <br />
        ///     Platforms: iOS <br />
        /// </summary>
        [JsonProperty("isIos")]
        public bool? DeliverToIos { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all devices registered under your app's Google
        ///     Android platform. <br />
        ///     Platforms: Android <br />
        /// </summary>
        [JsonProperty("isAndroid")]
        public bool? DeliverToAndroid { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all subscribed web browser users, including
        ///     Chrome, Firefox, and Safari. <br /> You may use this instead as a combined flag
        ///     instead of separately enabling isChromeWeb, isFirefox, and isSafari, though the three
        ///     options are equivalent to this one. <br />
        ///     Platforms: WEB <br />
        /// </summary>
        [JsonProperty("isAnyWeb")]
        public bool? DeliverToAnyWeb { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all Google Chrome, Chrome on Android, and Mozilla
        ///     Firefox users registered under your Chrome &amp; Firefox web push platform. <br />
        ///     Platforms: WEB <br />
        /// </summary>
        [JsonProperty("isChromeWeb")]
        public bool? DeliverToChromeWeb { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all Mozilla Firefox desktop users registered
        ///     under your Firefox web push platform. <br />
        ///     Platforms: WEB <br />
        /// </summary>
        [JsonProperty("isFirefox")]
        public bool? DeliverToFirefox { get; set; }

        /// <summary>
        ///     <br /> Does not support iOS Safari Indicates whether to send to all Apple's Safari
        ///     desktop users registered under your Safari web push platform. <br /> Read more about
        ///     <see cref="!:https://documentation.onesignal.com/docs/why-doesnt-web-push-work-with-ios">
        ///     iOS Safari </see> . <br />
        ///     Platforms: WEB <br />
        /// </summary>
        [JsonProperty("isSafari")]
        public bool? DeliverToSafari { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all devices registered under your app's Windows
        ///     Phone 8.0 platform. <br />
        ///     Platforms: Windows Phone 8.0 <br />
        /// </summary>
        [JsonProperty("isWP")]
        public bool? DeliverToWindowsPhone { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all devices registered under your app's Windows
        ///     Phone 8.1+ platform. <br />
        ///     Platforms: Windows Phone 8.1+ <br />
        /// </summary>
        [JsonProperty("isWP_WNS")]
        public bool? DeliverToWindowsRtPhone { get; set; }

        /// <summary>
        ///     <br /> Indicates whether to send to all devices registered under your app's Amazon
        ///     Fire platform. <br />
        ///     Platforms: Amazon <br />
        /// </summary>
        [JsonProperty("isAdm")]
        public bool? DeliverToAmazon { get; set; }

        /// <summary>
        ///     <br /> This flag is not used for web push Please see isChromeWeb for sending to web
        ///     push users. This flag only applies to Google Chrome Apps &amp; Extensions. <br />
        ///     Indicates whether to send to all devices registered under your app's Google Chrome
        ///     Apps &amp; Extension platform. <br />
        ///     Platforms: ChromeApp <br />
        /// </summary>
        [JsonProperty("isChrome")]
        public bool? DeliverToChrome { get; set; }
    }
}