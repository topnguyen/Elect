namespace Elect.Logger.Models.Event
{
    public class EventModel
    {
        public EventType Type { get; set; }

        public RefererModel Referer { get; set; }

        public UtmModel Utm { get; set; }
    }
}