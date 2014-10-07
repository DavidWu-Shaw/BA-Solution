using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Product : BusinessObject<ProductData>
    {
        [RequiredField("ProductNameRequired", "The name must be defined.")]
        [StringLength("ProductNameLength", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ProductDescriptionLength", "The Description must have a length less than {1}", MaxLength = 400)]
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

        public object CategoryId
        {
            get { return Data.CategoryId; }
            set { Data.CategoryId = value; }
        }

        public object SupplierId
        {
            get { return Data.SupplierId; }
            set { Data.SupplierId = value; }
        }

        public decimal UnitPrice
        {
            get { return Data.UnitPrice; }
            set { Data.UnitPrice = value; }
        }

        public byte[] Sketch
        {
            get { return Data.Sketch; }
            set { Data.Sketch = value; }
        }

        public object SellingPeriodId
        {
            get { return Data.SellingPeriodId; }
            set { Data.SellingPeriodId = value; }
        }

        public bool IsDiscontinued
        {
            get { return Data.IsDiscontinued; }
            set { Data.IsDiscontinued = value; }
        }

        public decimal DiscountAmount
        {
            get { return Data.DiscountAmount; }
            set { Data.DiscountAmount = value; }
        }

        [StringLength("ProductPackagingLength", "The Packaging must have a length less than {1}", MaxLength = 100)]
        public string Packaging
        {
            get { return Data.Packaging; }
            set { Data.Packaging = value; }
        }
        [StringLength("ProductUnitOfMeasureLength", "The UnitOfMeasure must have a length less than {1}", MaxLength = 10)]
        public string UnitOfMeasure
        {
            get { return Data.UnitOfMeasure; }
            set { Data.UnitOfMeasure = value; }
        }
    }
}
