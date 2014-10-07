using BA.UnityRegistry;
using CRM.Configuration;
using CRM.ShopComponent.Dto;
using Framework.Globalization;
using Framework.Unity;
using Framework.UoW;
using Microsoft.Practices.ServiceLocation;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Configuration;
using SFA.Web.Common;
using SFA.Web.Common.Binders;
using SFA.Web.Controllers;
using SubjectEngine.Configuration;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace SFA.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("",
                "Account/SignIn",
                new { culture = WebContext.Current.DefaultLanguage.Culture, controller = AccountController.ControllerName, action = AccountController.SignInAction }, // Parameter defaults
                new[] { "SFA.Web.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{culture}/{controller}/{action}/{id}", // URL with parameters
                new { culture = WebContext.Current.DefaultLanguage.Culture, controller = HomeController.ControllerName, action = HomeController.HomeAction, id = UrlParameter.Optional }, // Parameter defaults
                new[] { "SFA.Web.Controllers" }
            );

        }

        protected void Application_Start()
        {
            InitUnity();
            InitFramework();
            InitWebContext();
            StringLocalizer.LocalizeString += new EventHandler<LocalizeStringEventArgs>(StringLocalizer_LocalizeString);

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //HtmlHelper.UnobtrusiveJavaScriptEnabled = false;
            ModelBinders.Binders.Add(typeof(UserContext), new UserContextModelBinder());
            ModelBinders.Binders.Add(typeof(CartDto), new CartModelBinder());
        }

        private void InitWebContext()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                WebContext.Current.Initilize(uow);
            }
        }

        private void InitFramework()
        {
            IDataStoreManager storeManager = ServiceLocator.Current.GetInstance<IDataStoreManager>();
            EntityResolver entityResolver = ServiceLocator.Current.GetInstance<EntityResolver>();
            IBusinessObjectFactory factory = ServiceLocator.Current.GetInstance<IBusinessObjectFactory>();
            UnitOfWorkFactory.Instance.Initialize(storeManager, entityResolver, entityResolver, factory);
        }

        private void InitUnity()
        {
            IList<IUnityRegistry> registries = new List<IUnityRegistry>();
            registries.Add(new CoreRegistry());
            registries.Add(new SubjectEngineRepositoryRegistry());
            registries.Add(new SubjectEngineServiceRegistry());
            registries.Add(new SetupRepositoryRegistry());
            registries.Add(new SetupServiceRegistry());
            registries.Add(new CRMRepositoryRegistry());
            registries.Add(new CRMServiceRegistry());
            UnityLibrary library = new UnityLibrary(registries);
            library.InitializeServiceLocator();
        }

        private void StringLocalizer_LocalizeString(object sender, LocalizeStringEventArgs e)
        {
            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && e.LanguageId != null)
            {
                LanguageDto currentLanguage = WebContext.Current.LanguageDic[e.LanguageId];
                if (currentLanguage.SysPhrases.ContainsKey(e.Key))
                {
                    e.Value = currentLanguage.SysPhrases[e.Key];
                }
                else
                {
                    // Save this key and defaultvalue in memory
                    currentLanguage.SysPhrases.Add(e.Key, e.DefaultValue);
                    // Save this key and defaultvalue in Database
                    LanguagePhraseDto phrase = new LanguagePhraseDto();
                    phrase.LanguageId = currentLanguage.Id;
                    phrase.PhraseKey = e.Key;
                    phrase.PhraseValue = e.DefaultValue;
                    using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                    {
                        LanguagePhraseFacade facade = new LanguagePhraseFacade(uow);
                        facade.SaveLanguagePhrase(phrase);
                    }
                }
            }
        }
    }
}