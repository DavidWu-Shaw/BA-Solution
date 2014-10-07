using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SFA.Web.Common;
using Setup.Component;

namespace SFA.Web.Models
{
    public class BaseViewModel
    {
        private UserContext _currentUserContext;
        public UserContext CurrentUserContext
        {
            get
            {
                if (_currentUserContext == null)
                {
                    if (HttpContext.Current.Session[UserContext.UserContextStateKey] != null)
                    {
                        _currentUserContext = (UserContext)HttpContext.Current.Session[UserContext.UserContextStateKey];
                    }
                    else
                    {
                        _currentUserContext = new UserContext(new UserIdentity());
                        HttpContext.Current.Session[UserContext.UserContextStateKey] = _currentUserContext;
                    }
                }

                //if (_currentUserContext.IsAnonymous && !AllowAnonymous)
                //{
                //    // Redirect immediately
                //    Response.Redirect(UserLoginUrl, true);
                //}

                return _currentUserContext;
            }
        }

    }
}