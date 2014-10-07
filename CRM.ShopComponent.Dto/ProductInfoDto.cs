using System;
using System.Collections.Generic;

namespace CRM.ShopComponent.Dto
{
    public class ProductInfoDto
    {
        # region Properties name
        // Used for data binding, DataField
        public const string FLD_ProductId = "ProductId";
        public const string FLD_Name = "Name";
        public const string FLD_Description = "Description";
        public const string FLD_UnitPrice = "UnitPrice";
        public const string FLD_Packaging = "Packaging";
        public const string FLD_UnitOfMeasure = "UnitOfMeasure";
        public const string FLD_Sketch = "Sketch";
        public const string FLD_SupplierId = "SupplierId";
        public const string FLD_SupplierName = "SupplierName";
        public const string FLD_CategoryText = "CategoryText";

        # endregion Properties name

        public object ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object CategoryId { get; set; }
        public object CatalogId { get; set; }
        public object SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] Sketch { get; set; }
        public object SellingPeriodId { get; set; }
        public bool IsDiscontinued { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Packaging { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfRatings { get; set; }

        public string SupplierName { get; set; }
        public string CategoryText { get; set; }
        public string SellingPeriodName { get; set; }
        public DateTime? SellingPeriodStartTime { get; set; }
        public DateTime? SellingPeriodEndTime { get; set; }

        public List<ReviewInfoDto> Reviews { get; set; }
    }
}
