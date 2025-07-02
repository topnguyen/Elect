namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Class used to describe notification filter field type used in filter operations. 
    /// </summary>
    public enum NotificationFilterFieldTypeEnum
    {
        /// <summary>
        ///     relation = "&gt;" or "&lt;" <br /> hours_ago = number of hours before or after the
        ///     users last session. Example: "1.1"
        /// </summary>
        LastSession,
        /// <summary>
        ///     relation = "&gt;" or "&lt;" <br /> hours_ago = number of hours before or after the
        ///     users first session. Example: "1.1"
        /// </summary>
        FirstSession,
        /// <summary>
        ///     relation = "&gt;", "&lt;", "=" or "!=" <br /> value = number sessions. Example: "1" 
        /// </summary>
        SessionCount,
        /// <summary>
        ///     relation = "&gt;" or "&lt;" <br /> value = Time in seconds the user has been in your
        ///     app. Example: "3600"
        /// </summary>
        SessionTime,
        /// <summary>
        ///     relation = "&gt;", "&lt;", or "=" <br /> value = Amount in USD a user has spent on
        ///     IAP (In App Purchases). Example: "0.99"
        /// </summary>
        AmountSpent,
        /// <summary>
        ///     relation = "&gt;", "&lt;" or "=" <br /> key = SKU purchased in your app as an IAP (In
        ///     App Purchases). Example: "com.domain.100coinpack" <br /> value = value of SKU to
        ///     compare to. Example: "0.99"
        /// </summary>
        BoughtSku,
        /// <summary>
        ///     relation = "&gt;", "&lt;", "=", "!=", "exists" or "not_exists" <br /> key = Tag key
        ///     to compare. <br /> value = Tag value to compare. Not required for "exists" or
        ///     "not_exists". <br />
        ///     Example: See
        ///              <see cref="!:https://documentation.onesignal.com/reference#section-formatting-filters">
        ///              Formatting Filters </see>
        /// </summary>
        Tag,
        /// <summary>
        ///     relation = "=" or "!=" <br /> value = 2 character language code. Example: "en".
        ///     <br /> For a list of all language codes go
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here </see>
        /// </summary>
        Language,
        /// <summary>
        ///     relation = "&gt;", "&lt;", "=" or "!=" <br /> value = app version. Example: "1.0.0" 
        /// </summary>
        AppVersion,
        /// <summary>
        ///     radius = in meters <br /> lat = latitude <br /> long = longitude <br /> 
        /// </summary>
        Location,
        /// <summary>
        ///     value = email address 
        /// </summary>
        Email
    }
}
