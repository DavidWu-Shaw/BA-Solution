using System;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class ScheduleEventDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime ScheduledTime { get; set; }
        public object ObjectId { get; set; }
        public int ObjectTypeId { get; set; }
        public int ReccuringTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
