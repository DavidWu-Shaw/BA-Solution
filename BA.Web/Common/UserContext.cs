using System;
using System.Globalization;
using System.Security.Principal;
using CRM.ShopComponent.Dto;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Core;

namespace BA.Web.Common
{
    public class UserContext : IPrincipal, IDisposable
    {
        public const string UserContextStateKey = "UserContextStateKey";

        public UserIdentity User { get; set; }
        public LanguageDto CurrentLanguage { get; set; }
        public CultureInfo CurrentCultureInfo { get; set; }
        public CartDto ShoppingCart { get; set; }

        public UserContext(UserIdentity user)
        {
            Initilize(user);
        }

        public void Initilize(UserIdentity user)
        {
            User = user;
            InitilizeShoppingCart();
            SetCurrentLanguage(user.LanguageId);
        }

        public void SetCurrentLanguage(object languageId)
        {
            if (languageId != null)
            {
                if (WebContext.Current.LanguageList.ContainsKey(languageId))
                {
                    CurrentLanguage = WebContext.Current.LanguageList[languageId];
                }
            }
            else
            {
                CurrentLanguage = WebContext.Current.LanguageList[WebContext.Current.ApplicationOption.DefaultLanguageId];
            }
            CurrentCultureInfo = new CultureInfo(CurrentLanguage.Culture);
        }

        public void InitilizeShoppingCart()
        {
            ShoppingCart = new CartDto();
        }

        //private void StringLocalizer_LocalizeString(object sender, LocalizeStringEventArgs e)
        //{
        //    if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported)
        //    {
        //        if (CurrentLanguage.SysPhrases.ContainsKey(e.Key))
        //        {
        //            e.Value = CurrentLanguage.SysPhrases[e.Key];
        //        }
        //        else
        //        {
        //            // Save this key and defaultvalue in memory
        //            CurrentLanguage.SysPhrases.Add(e.Key, e.DefaultValue);
        //            // Save this key and defaultvalue in Database
        //            LanguagePhraseDto phrase = new LanguagePhraseDto();
        //            phrase.LanguageId = CurrentLanguage.Id;
        //            phrase.PhraseKey = e.Key;
        //            phrase.PhraseValue = e.DefaultValue;
        //            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
        //            {
        //                LanguagePhraseFacade facade = new LanguagePhraseFacade(uow);
        //                facade.SaveLanguagePhrase(phrase);
        //            }
        //        }
        //    }
        //}

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
        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return User; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}