using Framework.Core;

namespace CRM.Component.Dto
{
    public class TransactionItemDto : BaseDto
    {
        public string ItemDescription { get; set; }
        public object ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
