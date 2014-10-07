using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SFA.Web.MvcExtensions.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var routeData = filterContext.Controller.ControllerContext.RouteData.Values;

            object queryCulture;

            if (routeData.TryGetValue("culture", out queryCulture))
            {
                try
                {
                    //CultureInfo newCulture = queryCulture as CultureInfo;

                    //if (newCulture == null)
                    //{
                    //    newCulture = CultureInfo.GetCultureInfo(queryCulture.ToString());
                    //}

                    //if (UserCultureService.SupportedCultures.ContainsKey(newCulture))
                    //{
                    //    UserCultureService.SetUserCulture(newCulture);
                    //}
                }
                catch
                {
                    // ignore the culture if the format is wrong
                }
            }
        }
    }

}