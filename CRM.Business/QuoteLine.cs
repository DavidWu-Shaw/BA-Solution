using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class QuoteLine : ChildBusinessObject<Quote, QuoteLineData>
    {
        public object ProductId
        {
            get { return Data.ProductId; }
            set { Data.ProductId = value; }
        }

        public decimal TargetPrice
        {
            get { return Data.TargetPrice; }
            set { Data.TargetPrice = value; }
        }

        public int Quantity
        {
            get { return Data.Quantity; }
            set { Data.Quantity = value; }
        }
    }
}
