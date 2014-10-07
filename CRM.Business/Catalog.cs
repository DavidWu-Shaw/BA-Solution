using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Catalog : BusinessObject<CatalogData>
    {
        [StringLength("CatalogNameLength", "The Name must have a length less than {1}", MaxLength = 100)]
        public string Name
        {
            get { return Data.Name; }
            set { Data.Name = value; }
        }

        [StringLength("CatalogDescriptionLength", "The Description must have a length less than {1}", MaxLength = 100)]
        public string Description
        {
            get { return Data.Description; }
            set { Data.Description = value; }
        }

        public bool IsDiscontinued
        {
            get { return Data.IsDiscontinued; }
            set { Data.IsDiscontinued = value; }
        }
        public bool IsGlobal
        {
            get { return Data.IsGlobal; }
            set { Data.IsGlobal = value; }
        }
    }
}
