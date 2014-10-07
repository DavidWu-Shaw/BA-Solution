using Framework.Data;

namespace CRM.Data
{
    public class TransactionItemData : ChildDataObject
    {
        public virtual string ItemDescription { get; set; }
        public virtual object ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual decimal Amount { get; set; }
    }
}
