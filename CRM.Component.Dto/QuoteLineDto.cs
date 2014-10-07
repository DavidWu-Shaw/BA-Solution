using Framework.Core;

namespace CRM.Component.Dto
{
    public class QuoteLineDto : BaseDto
    {
        public object ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TargetPrice { get; set; }
    }
}
