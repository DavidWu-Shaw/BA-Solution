using Framework.Core;

namespace CRM.ShopComponent.Dto
{
    public class CartItemDto : BaseDto
    {
        public object SupplierId { get; set; }
        public string SupplierName { get; set; }
        public object ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QtyOrdered { get; set; }
        public decimal Amount { get; set; }
    }
}
