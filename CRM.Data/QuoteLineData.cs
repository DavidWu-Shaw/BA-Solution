using Framework.Data;

namespace CRM.Data
{
    public class QuoteLineData : ChildDataObject
    {
        public virtual object ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal TargetPrice { get; set; }
    }
}
