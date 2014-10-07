using Framework.Data;

namespace CRM.Data
{
    public class OpportunityData : DataObject
    {
        public OpportunityData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual object ContactId { get; set; }
        public virtual object CustomerId { get; set; }
        public virtual object ProductId { get; set; }
        public virtual decimal EstimateAmount { get; set; }
    }
}
