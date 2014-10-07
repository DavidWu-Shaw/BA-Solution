using Framework.Core;

namespace CRM.Component.Dto
{
    public class OpportunityDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object ContactId { get; set; }
        public object CustomerId { get; set; }
        public object ProductId { get; set; }
        public decimal EstimateAmount { get; set; }
    }
}
