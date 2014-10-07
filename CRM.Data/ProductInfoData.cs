using Framework.Data;
using System;

namespace CRM.Data
{
    public class ProductInfoData
    {
        public virtual object ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual object CategoryId { get; set; }
        public virtual object CatalogId { get; set; }
        public virtual object SupplierId { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual byte[] Sketch { get; set; }
        public virtual object SellingPeriodId { get; set; }
        public virtual bool IsDiscontinued { get; set; }
        public virtual decimal DiscountAmount { get; set; }
        public virtual string Packaging { get; set; }
        public virtual string UnitOfMeasure { get; set; }
        public virtual decimal Rating { get; set; }
        public virtual int NumberOfRatings { get; set; }

        public virtual string SupplierName { get; set; }
        public virtual string CategoryText { get; set; }
        public virtual string SellingPeriodName { get; set; }
        public virtual DateTime? SellingPeriodStartTime { get; set; }
        public virtual DateTime? SellingPeriodEndTime { get; set; }
    }
}
