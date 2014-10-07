using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.ShopComponent.Dto;

namespace SFA.Web.Common.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "ShoppingCart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            CartDto cart = (CartDto)controllerContext.HttpContext.Session[sessionKey];
            // create the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new CartDto();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            // return the cart        
            return cart;
        }
    }
}