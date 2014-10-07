using System;
using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class SellingPeriodData : DataObject
    {
        public SellingPeriodData()
        {
        }

        public virtual string Name { get; set; }
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
    }
}
