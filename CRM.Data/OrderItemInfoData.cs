﻿using Framework.Data;

namespace CRM.Data
{
    public class OrderItemInfoData
    {
        public virtual object OrderItemId { get; set; }
        public virtual string ItemDescription { get; set; }
        public virtual object ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual int QtyOrdered { get; set; }
        public virtual decimal Amount { get; set; }
    }
}
