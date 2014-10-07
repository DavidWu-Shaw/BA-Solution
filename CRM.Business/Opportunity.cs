using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Opportunity : BusinessObject<OpportunityData>
    {
        [RequiredField("OpportunitytNameRequired", "The name must be defined.")]
        [StringLength("OpportunityNameLength", "The name must have a length less than {1}", MaxLength = 100)]
        public string Name
        {
            get
            {
                return Data.Name;
            }
            set
            {
                Data.Name = value;
            }
        }

        [StringLength("OpportunityDescriptionLength", "The Description must have a length less than {1}", MaxLength = 100)]
        public string Description
        {
            get
            {
                return Data.Description;
            }
            set
            {
                Data.Description = value;
            }
        }

        public object ContactId
        {
            get { return Data.ContactId; }
            set { Data.ContactId = value; }
        }

        public object CustomerId
        {
            get { return Data.CustomerId; }
            set { Data.CustomerId = value; }
        }

        public object ProductId
        {
            get { return Data.ProductId; }
            set { Data.ProductId = value; }
        }

        public decimal EstimateAmount
        {
            get { return Data.EstimateAmount; }
            set { Data.EstimateAmount = value; }
        }
    }
}
