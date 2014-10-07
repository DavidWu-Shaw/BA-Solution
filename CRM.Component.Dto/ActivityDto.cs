using System;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class ActivityDto : BaseDto
    {
        public string ActivityName { get; set; }
        public object EmployeeId { get; set; }
        public object ContactId { get; set; }
        public object CustomerId { get; set; }
        public object ActivityTypeId { get; set; }
        public string Notes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal TimeSpent { get; set; }
    }
}
