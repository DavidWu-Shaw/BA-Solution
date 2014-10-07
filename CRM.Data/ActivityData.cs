using System;
using Framework.Data;

namespace CRM.Data
{
    public class ActivityData : DataObject
    {
        public ActivityData()
        {
        }

        public virtual string ActivityName { get; set; }
        public virtual object EmployeeId { get; set; }
        public virtual object ContactId { get; set; }
        public virtual object CustomerId { get; set; }
        public virtual object ActivityTypeId { get; set; }
        public virtual string Notes { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual decimal TimeSpent { get; set; }
    }
}
