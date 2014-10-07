using Framework.Business;
using Setup.Data;

namespace Setup.Business
{
    public class DomainSetupMenu : ChildBusinessObject<Domain, DomainSetupMenuData>
    {
        public object SetupMenuId
        {
            get { return Data.SetupMenuId; }
            set { Data.SetupMenuId = value; }
        }

        public int Sort
        {
            get { return Data.Sort; }
            set { Data.Sort = value; }
        }
    }
}
