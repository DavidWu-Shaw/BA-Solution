using System;
using Framework.Data;

namespace CRM.Data
{
    public class CategoryData : DataObject
    {
        public virtual string Name { get; set; }
        public virtual object ParentId { get; set; }
        public virtual object CatalogId { get; set; }
    }
}
