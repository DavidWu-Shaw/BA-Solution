using Framework.Core;

namespace CRM.Component.Dto
{
    public class ProductDto : BaseDto
    {
        # region Properties name

        public const string FLD_Name = "Name";
        public const string FLD_Description = "Description";
        public const string FLD_UnitPrice = "UnitPrice";
        public const string FLD_Sketch = "Sketch";
        public const string FLD_SupplierId = "SupplierId";
        public const string FLD_CategoryId = "CategoryId";

        # endregion Properties name

        public string Name { get; set; }
        public string Description { get; set; }
        public object CategoryId { get; set; }
        public object SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] Sketch { get; set; }
        public object SellingPeriodId { get; set; }
        public bool IsDiscontinued { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Packaging { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
