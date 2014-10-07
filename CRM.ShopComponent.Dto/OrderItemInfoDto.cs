using System;

namespace CRM.ShopComponent.Dto
{
    [Serializable]
    public class OrderItemInfoDto
    {
        public const string FLD_OrderItemId = "OrderItemId";

        public object OrderItemId { get; set; }
        public string ItemDescription { get; set; }
        public object ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QtyOrdered { get; set; }
        public decimal Amount { get; set; }
    }
}
