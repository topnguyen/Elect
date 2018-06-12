using System;
using System.Collections.Generic;
using Elect.Logger.Models.Event.Utils;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Models.Event
{
    [Serializable]
    public class EventModel
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;

        public EventType Type { get; set; } = EventType.Visit;

        // Visit

        public UtmModel Utm { get; set; }

        public RefererModel Referer { get; set; }

        public DateTimeOffset StartedTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        ///     Only Visit have End Time
        /// </summary>
        public DateTimeOffset? EndedTime { get; set; }

        // Data

        /// <summary>
        ///     Canbe a Page/Screen/Popup or Element in a Screen
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        ///     Descart Latitude of element in pixel
        /// </summary>
        public double ElementDescartLatPx { get; set; }

        /// <summary>
        ///     Descart Longitude of element in pixel
        /// </summary>
        public double ElementDescartLngPx { get; set; }

        /// <summary>
        ///     Screen width in pixel
        /// </summary>
        public double ScreenWidthPx { get; set; }

        /// <summary>
        ///     Screen height in pixel
        /// </summary>
        public double ScreenHeightPx { get; set; }

        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

        public EventModel()
        {
        }

        public EventModel(HttpRequest httpRequest)
        {
            if (httpRequest == null)
            {
                return;
            }

            Utm = UtmHelper.Get(httpRequest);
            Referer = RefererHelper.Get(httpRequest);
        }

        public EventModel(HttpContext httpContext) : this(httpContext?.Request)
        {
        }
    }
}