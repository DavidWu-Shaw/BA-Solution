using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SFA.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public const string ControllerName = "Home";
        public const string HomeAction = "Index";
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
