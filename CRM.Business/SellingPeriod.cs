using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class SellingPeriod : BusinessObject<SellingPeriodData>
    {
        [RequiredField("SellingPeriodNameRequired", "The Name must be defined.")]
        [StringLength("SellingPeriodNameLength", "The Name must have a length less than {1}", MaxLength = 50)]
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

        public DateTime? StartTime
        {
            get { return Data.StartTime; }
            set { Data.StartTime = value; }
        }

        public DateTime? EndTime
        {
            get { return Data.EndTime; }
            set { Data.EndTime = value; }
        }
    }
}
