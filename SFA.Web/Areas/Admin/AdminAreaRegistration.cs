using System.Web.Mvc;
using SFA.Web.Common;
using SFA.Web.Areas.Admin.Controllers;

namespace SFA.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "{culture}/Admin/{controller}/{action}/{id}",
                new { culture = WebContext.Current.DefaultLanguage.Culture, controller = HomeController.ControllerName, action = HomeController.HomeAction, id = UrlParameter.Optional }
            );
        }
    }
}
