using System;

namespace CRM.Component.Dto
{
    public class CalendarEventDto
    {
        public string EventSubject { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string ObjectType { get; set; }
        public string ObjectDisplay { get; set; }
        public object ObjectId { get; set; }
        public object ScheduleEventId { get; set; }
    }
}
