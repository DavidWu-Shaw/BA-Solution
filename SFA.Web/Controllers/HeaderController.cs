using System.Collections.Generic;
using System.Web.Mvc;
using Setup.Component.Dto;
using SFA.Web.Common;

namespace SFA.Web.Controllers
{
    public class HeaderController : BaseController
    {
        public const string ControllerName = "Header";
        public const string MenuAction = "Menu";

        public HeaderController()
        {
        }

        public PartialViewResult Menu()
        {
            IEnumerable<MainMenuDto> accessableMainMenus = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].MainMenus;
            return PartialView(accessableMainMenus);
        }
    }
}
