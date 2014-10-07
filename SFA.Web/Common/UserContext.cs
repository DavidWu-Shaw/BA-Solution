using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Setup.Component;
using Setup.Component.Dto;
using System.Globalization;
using CRM.ShopComponent.Dto;
using Setup.Core;

namespace SFA.Web.Common
{
    public class UserContext
    {
        public const string UserContextStateKey = "UserContextStateKey";

        public UserIdentity User { get; set; }
        //public LanguageDto CurrentLanguage { get; set; }
        //public CultureInfo CurrentCultureInfo { get; set; }
        public CartDto ShoppingCart { get; set; }

        public UserContext(UserIdentity user)
        {
            Initilize(user);
        }

        public void Initilize(UserIdentity user)
        {
            User = user;
            InitilizeShoppingCart();
            //SetCurrentLanguage(user.LanguageId);
        }

        //public void SetCurrentLanguage(object languageId)
        //{
        //    if (languageId != null)
        //    {
        //        if (WebContext.Current.LanguageDic.ContainsKey(languageId))
        //        {
        //            CurrentLanguage = WebContext.Current.LanguageDic[languageId];
        //        }
        //    }
        //    else
        //    {
        //        CurrentLanguage = WebContext.Current.LanguageDic[WebContext.Current.ApplicationOption.DefaultLanguageId];
        //    }
        //    CurrentCultureInfo = new CultureInfo(CurrentLanguage.Culture);
        //}

        //public void SetCurrentLanguage(string cultureName)
        //{
        //    if (WebContext.Current.LanguageDicByCulture.ContainsKey(cultureName))
        //    {
        //        CurrentLanguage = WebContext.Current.LanguageDicByCulture[cultureName];
        //    }
        //    CurrentCultureInfo = new CultureInfo(CurrentLanguage.Culture);
        //}

        public void InitilizeShoppingCart()
        {
            ShoppingCart = new CartDto();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool IsAnonymous
        {
            get { return User.DomainId == (int)UserDomains.Anonymous; }
        }

        public bool IsMember
        {
            get { return User.DomainId == (int)UserDomains.Member; }
        }

        public bool IsSuperAdmin
        {
            get { return User.DomainId == (int)UserDomains.SuperAdmin; }
        }

        public bool IsAdmin
        {
            get { return User.DomainId == (int)UserDomains.SysAdmin || User.DomainId == (int)UserDomains.SuperAdmin; }
        }

        public bool IsCustomer
        {
            get { return User.DomainId == (int)UserDomains.Customer; }
        }

        public bool IsEmployee
        {
            get { return User.DomainId == (int)UserDomains.Employee; }
        }

        public bool IsSupplierAdmin
        {
            get { return User.DomainId == (int)UserDomains.SupplierAdmin; }
        }

        public bool IsSupplier
        {
            get { return User.DomainId == (int)UserDomains.Supplier || IsSupplierAdmin; }
        }

        public bool IsCartEmpty
        {
            get
            {
                return ShoppingCart.CartItems.Count == 0;
            }
        }
    }
}