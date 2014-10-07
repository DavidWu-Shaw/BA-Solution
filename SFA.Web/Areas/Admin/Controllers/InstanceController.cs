using System.Collections.Generic;
using System.Web.Mvc;
using Framework.Core;
using SFA.Web.Controllers;
using CRM.Core;
using Framework.Validation;
using System;
using SFA.Web.Areas.Admin.Models;

namespace SFA.Web.Areas.Admin.Controllers
{
    public class InstanceController : BaseController
    {
        protected InstanceTypes InstanceType { get; set; }

        protected ActionResult ListView(ListViewModel model)
        {
            model.CurrentLanguageId = CurrentLanguageId;
            return View("InstanceList", model);
        }

        protected ActionResult DetailView(InstanceViewModel model)
        {
            model.CurrentLanguageId = CurrentLanguageId;
            return View("InstanceDetail", model);
        }

        protected ActionResult EditView(InstanceViewModel model)
        {
            model.CurrentLanguageId = CurrentLanguageId;
            return View("InstanceEdit", model);
        }

        private void InitViewData()
        {
            ViewData["InstanceType"] = InstanceType;
            ViewData["LanguageId"] = CurrentLanguageId;
        }
    }
}