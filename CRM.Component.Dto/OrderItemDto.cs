using Framework.Core;

namespace CRM.Component.Dto
{
    public class OrderItemDto : BaseDto
    {
        public string ItemDescription { get; set; }
        public object ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QtyOrdered { get; set; }
        public decimal Amount { get; set; }
    }
}
