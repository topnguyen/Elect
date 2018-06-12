using System;
using Elect.Logger.Models.Event;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Event
{
    public class ElectEvent
    {
        public Func<EventModel, EventModel> BeforeLog { get; set; }

        public Func<EventModel, EventModel> AfterLog { get; set; }

        public EventModel Capture(object obj, HttpContext httpContent)
        {
            var @event = new EventModel();

            if (BeforeLog != null)
            {
                @event = BeforeLog(@event);
            }
            
            // TODO Capture Here
            
            if (AfterLog != null)
            {
                @event = AfterLog(@event);
            }

            return @event;
        }
    }
}