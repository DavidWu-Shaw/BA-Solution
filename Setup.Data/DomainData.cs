using System;
using Framework.Data;
using System.Collections.Generic;

namespace Setup.Data
{
    public class DomainData : DataObject
    {
        public DomainData()
        {
            DomainMainMenusData = new List<DomainMainMenuData>();
            DomainSetupMenusData = new List<DomainSetupMenuData>();
        }

        public virtual string Name { get; set; }
        public virtual string RelatedSubjectIdField { get; set; }
        public virtual string DefaultUrl { get; set; }

        public virtual IList<DomainMainMenuData> DomainMainMenusData { get; set; }
        public virtual IList<DomainSetupMenuData> DomainSetupMenusData { get; set; }
    }
}
