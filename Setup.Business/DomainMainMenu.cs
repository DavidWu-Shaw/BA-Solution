using Framework.Business;
using Setup.Data;

namespace Setup.Business
{
    public class DomainMainMenu : ChildBusinessObject<Domain, DomainMainMenuData>
    {
        public object MainMenuId
        {
            get { return Data.MainMenuId; }
            set { Data.MainMenuId = value; }
        }

        public int Sort
        {
            get { return Data.Sort; }
            set { Data.Sort = value; }
        }
    }
}
