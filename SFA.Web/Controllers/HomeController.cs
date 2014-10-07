using System.Web.Mvc;
using SFA.Web.Models;
using SFA.Web.Common;

namespace SFA.Web.Controllers
{
    public class HomeController : BaseController
    {
        public const string ControllerName = "Home";
        public const string HomeAction = "Home";
        public const string AboutAction = "About";
        public const string ContactUsAction = "ContactUs";

        // GET: /Home/
        public ActionResult Index()
        {
            return View("Index", GetModel(HomeAction, ""));
        }

        public ActionResult Home()
        {
            return View("Index", GetModel(HomeAction, ""));
        }

        public ActionResult About()
        {
            return View("Index", GetModel(AboutAction, ""));
        }

        public ActionResult ContactUs()
        {
            return View("Index", GetModel(ContactUsAction, ""));
        }

        [HttpPost]
        public ActionResult ContactUs(HomeViewModel model)
        {
            return View("Index");
        }

        private HomeViewModel GetModel(string action, string messageKey)
        {
            HomeViewModel model = new HomeViewModel();
            model.CurrentAction = action;
            model.CurrentContent = MvcHtmlString.Create(WebContext.Current.HomeContentTemplate.Body);
            return model;
        }
    }
}
