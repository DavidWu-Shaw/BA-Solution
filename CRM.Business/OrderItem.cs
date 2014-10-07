using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class OrderItem : ChildBusinessObject<Order, OrderItemData>
    {
        [StringLength("OrderItemItemDescriptionLength", "The ItemDescription must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("OrderItemProductNameLength", "The ProductName must have a length less than {1}", MaxLength = 100)]
        public string ProductName
        {
            get { return Data.ProductName; }
            set { Data.ProductName = value; }
        }

        public decimal UnitPrice
        {
            get { return Data.UnitPrice; }
            set { Data.UnitPrice = value; }
        }

        public int QtyOrdered
        {
            get { return Data.QtyOrdered; }
            set { Data.QtyOrdered = value; }
        }

        public decimal Amount
        {
            get { return Data.Amount; }
            set { Data.Amount = value; }
        }
    }
}
