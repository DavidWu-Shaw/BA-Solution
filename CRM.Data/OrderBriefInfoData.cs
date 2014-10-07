using System;
using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class OrderBriefInfoData
    {
        public virtual object OrderId { get; set; }
        public virtual string OrderNumber { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual int StatusId { get; set; }

        public virtual string StatusText { get; set; }
    }
}
