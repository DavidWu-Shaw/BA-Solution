using System;
using System.Collections.Generic;
using CRM.Core;

namespace CRM.ShopComponent.Dto
{
    [Serializable]
    public class OrderInfoDto
    {
        # region Properties name

        public const string FLD_OrderId = "OrderId";
        public const string FLD_OrderNumber = "OrderNumber";
        public const string FLD_StatusId = "StatusId";
        public const string FLD_Amount = "Amount";

        # endregion Properties name

        public object OrderId { get; set; }
        public string OrderNumber { get; set; }
        public object CustomerId { get; set; }
        public object ContactId { get; set; }
        public DateTime TimeOrdered { get; set; }
        public DateTime? TimeShipped { get; set; }
        public DateTime? TimeShipBy { get; set; }
        public DateTime? TimeCancelBy { get; set; }
        public decimal Amount { get; set; }
        public int QtyOrderedTotal { get; set; }
        public object CurrencyId { get; set; }
        public object SupplierId { get; set; }
        public object ShipToId { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }
        public string NotificationEmail { get; set; }

        public string SupplierName { get; set; }
        public string StatusText { get; set; }

        public ShipToDto ShipTo { get; set; }

        public List<OrderItemInfoDto> OrderItems { get; set; }
        public List<OrderCommand> OrderCommands { get; set; }
    }
}
