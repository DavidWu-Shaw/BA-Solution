using System;
using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class OrderData : DataObject
    {
        public OrderData()
        {
            OrderItemsData = new List<OrderItemData>();
        }

        public virtual string OrderNumber { get; set; }
        public virtual object CustomerId { get; set; }
        public virtual object ContactId { get; set; }
        public virtual DateTime TimeOrdered { get; set; }
        public virtual DateTime? TimeShipped { get; set; }
        public virtual DateTime? TimeShipBy { get; set; }
        public virtual DateTime? TimeCancelBy { get; set; }
        public virtual int QtyOrderedTotal { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual object CurrencyId { get; set; }
        public virtual object SupplierId { get; set; }
        public virtual object BillToId { get; set; }
        public virtual object ShipToId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual string Notes { get; set; }
        public virtual string ShipToContactPhone { get; set; }
        public virtual string ShipToContactPerson { get; set; }
        public virtual string ShipToAddress { get; set; }
        public virtual string ShipToZipCode { get; set; }

        public virtual IList<OrderItemData> OrderItemsData { get; set; }
    }
}
