using System;
using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class OrderDto : BaseDto
    {
        # region Properties name

        public const string FLD_OrderNumber = "OrderNumber";
        public const string FLD_StatusId = "StatusId";
        public const string FLD_Amount = "Amount";

        # endregion Properties name

        public string OrderNumber { get; set; }
        public object CustomerId { get; set; }
        public object ContactId { get; set; }
        public DateTime TimeOrdered { get; set; }
        public DateTime? TimeShipped { get; set; }
        public DateTime? TimeShipBy { get; set; }
        public DateTime? TimeCancelBy { get; set; }
        public int QtyOrderedTotal { get; set; }
        public decimal Amount { get; set; }
        public object CurrencyId { get; set; }
        public object SupplierId { get; set; }
        public object BillToId { get; set; }
        public object ShipToId { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
