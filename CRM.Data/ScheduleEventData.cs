using System;
using Framework.Data;

namespace CRM.Data
{
    public class ScheduleEventData : DataObject
    {
        public virtual string Name { get; set; }
        public virtual DateTime ScheduledTime { get; set; }
        public virtual object ObjectId { get; set; }
        public virtual int ObjectTypeId { get; set; }
        public virtual int ReccuringTypeId { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
    }
}
