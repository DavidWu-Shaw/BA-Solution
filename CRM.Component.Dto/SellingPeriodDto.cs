using System;
using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class SellingPeriodDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
