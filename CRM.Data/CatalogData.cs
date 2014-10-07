using System;
using Framework.Data;

namespace CRM.Data
{
    public class CatalogData : DataObject
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsGlobal { get; set; }
        public virtual bool IsDiscontinued { get; set; }
    }
}
