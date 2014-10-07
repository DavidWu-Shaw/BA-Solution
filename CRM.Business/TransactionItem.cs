using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class TransactionItem : ChildBusinessObject<Transaction, TransactionItemData>
    {
        [StringLength("TransactionItemItemDescriptionLength", "The ItemDescription must have a length less than {1}", MaxLength = 100)]
        public string ItemDescription
        {
            get
            {
                return Data.ItemDescription;
            }
            set
            {
                Data.ItemDescription = value;
            }
        }

        public object ProductId
        {
            get { return Data.ProductId; }
            set { Data.ProductId = value; }
        }

        public int Quantity
        {
            get
            {
                return Data.Quantity;
            }
            set
            {
                Data.Quantity = value;
            }
        }

        public decimal UnitPrice
        {
            get { return Data.UnitPrice; }
            set { Data.UnitPrice = value; }
        }

        public decimal Amount
        {
            get
            {
                return Data.Amount;
            }
            set
            {
                Data.Amount = value;
            }
        }
    }
}
