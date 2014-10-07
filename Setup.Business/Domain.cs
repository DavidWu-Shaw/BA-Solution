using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Business;
using Setup.Data;
using Framework.Validation;
using Framework.Security;

namespace Setup.Business
{
    public class Domain : BusinessObject<DomainData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            DomainMainMenus = new ChildBoCollection<DomainData, DomainMainMenu, DomainMainMenuData>(Service, Data.DomainMainMenusData, this);
            DomainSetupMenus = new ChildBoCollection<DomainData, DomainSetupMenu, DomainSetupMenuData>(Service, Data.DomainSetupMenusData, this);
        }

        [ChildCollection]
        public IBusinessObjectCollection<DomainMainMenu> DomainMainMenus
        {
            get;
            private set;
        }

        [ChildCollection]
        public IBusinessObjectCollection<DomainSetupMenu> DomainSetupMenus
        {
            get;
            private set;
        }

        [RequiredField("DomainNameRequired", "The Name must be defined.")]
        [StringLength("DomainNameLength", "The Name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("DomainRelatedSubjectIdFieldLength", "The RelatedSubjectIdField must have a length less than {1}", MaxLength = 50)]
        public string RelatedSubjectIdField
        {
            get
            {
                return Data.RelatedSubjectIdField;
            }
            set
            {
                Data.RelatedSubjectIdField = value;
            }
        }
    }
}
