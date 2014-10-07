using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Setup.Component;

namespace SFA.Web.Common.Binders
{
    public class UserContextModelBinder : IModelBinder
    {
        private const string sessionKey = "UserContextStateKey";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            UserContext userContext = (UserContext)controllerContext.HttpContext.Session[sessionKey];
            // create the Cart if there wasn't one in the session data
            if (userContext == null)
            {
                userContext = new UserContext(new UserIdentity());
                controllerContext.HttpContext.Session[sessionKey] = userContext;
            }
            // return the cart        
            return userContext;
        }
    }
}