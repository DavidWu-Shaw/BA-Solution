using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.WebServices;
using CRM.Configuration;
using Framework.Globalization;
using Framework.Unity;
using Framework.UoW;
using Microsoft.Practices.ServiceLocation;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Configuration;
using SubjectEngine.Configuration;
using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace BA.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            InitUnity();
            InitFramework();

            InitWebContext();
            StringLocalizer.LocalizeString += new EventHandler<LocalizeStringEventArgs>(StringLocalizer_LocalizeString);
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute("WebServices/CategoryWebService", new WebServiceHostFactory(), typeof(CategoryWebService)));

            //routes.MapPageRoute("", "Supplier/{SupplierId}", "~/Shop/SupplierPage.aspx");
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

        private void InitWebContext()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                WebContext.Current.Initilize(uow);
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = HttpContext.Current.Server.GetLastError();

            Server.Transfer("~" + GeneralErrorPage.PageUrl);

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        private void StringLocalizer_LocalizeString(object sender, LocalizeStringEventArgs e)
        {
            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && e.LanguageId != null)
            {
                LanguageDto currentLanguage = WebContext.Current.LanguageList[e.LanguageId];
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
