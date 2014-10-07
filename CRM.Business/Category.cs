using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Category : BusinessObject<CategoryData>
    {
        [StringLength("CategoryNameLength", "The Name must have a length less than {1}", MaxLength = 50)]
        public string Name
        {
            get { return Data.Name; }
            set { Data.Name = value; }
        }

        public object ParentId
        {
            get { return Data.ParentId; }
            set { Data.ParentId = value; }
        }

        public object CatalogId
        {
            get { return Data.CatalogId; }
            set { Data.CatalogId = value; }
        }
    }
}
