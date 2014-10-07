using Framework.Data;

namespace CRM.Data
{
    public class ProductData : DataObject
    {
        public ProductData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual object CategoryId { get; set; }
        public virtual object SupplierId { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual byte[] Sketch { get; set; }
        public virtual object SellingPeriodId { get; set; }
        public virtual bool IsDiscontinued { get; set; }
        public virtual decimal DiscountAmount { get; set; }
        public virtual string Packaging { get; set; }
        public virtual string UnitOfMeasure { get; set; }
    }
}
