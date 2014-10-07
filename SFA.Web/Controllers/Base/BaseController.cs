using System.Web.Mvc;
using Setup.Component;
using SFA.Web.Common;
using SFA.Web.MvcExtensions.ActionFilters;
using Framework.Validation;
using System;

namespace SFA.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public object CurrentLanguageId { get; set; }

        private UserContext _currentUserContext;
        public UserContext CurrentUserContext
        {
            get
            {
                if (_currentUserContext == null)
                {
                    if (System.Web.HttpContext.Current.Session[UserContext.UserContextStateKey] != null)
                    {
                        _currentUserContext = (UserContext)System.Web.HttpContext.Current.Session[UserContext.UserContextStateKey];
                    }
                    else
                    {
                        _currentUserContext = new UserContext(new UserIdentity());
                        System.Web.HttpContext.Current.Session[UserContext.UserContextStateKey] = _currentUserContext;
                    }
                }

                return _currentUserContext;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var routeData = filterContext.Controller.ControllerContext.RouteData.Values;
            object queryCulture;
            if (routeData.TryGetValue("culture", out queryCulture))
            {
                string cultureName = queryCulture.ToString();
                //CurrentUserContext.SetCurrentLanguage(cultureName);

                if (WebContext.Current.LanguageDicByCulture.ContainsKey(cultureName))
                {
                    CurrentLanguageId = WebContext.Current.LanguageDicByCulture[cultureName].Id;
                }
                else
                {
                    CurrentLanguageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
                }
            }
        }

        protected void ProcUpdateResult(ValidationResult validationResult, Exception exception)
        {
            if (exception != null)
            {
                throw exception;
            }
            else
            {
                foreach (ValidationItem item in validationResult.Items)
                {
                    ModelState.AddModelError(item.Key, item.Message);
                }
            }
        }
    }
}