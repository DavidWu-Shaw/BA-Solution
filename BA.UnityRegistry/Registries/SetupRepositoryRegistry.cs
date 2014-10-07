using Framework.Unity;
using Microsoft.Practices.Unity;
using Setup.Repository.Contract;
using Setup.Repository;

namespace BA.UnityRegistry
{
    public class SetupRepositoryRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IMainMenuRepository, MainMenuRepository>();
            container.RegisterType<IDomainRepository, DomainRepository>();
            container.RegisterType<ISetupMenuRepository, SetupMenuRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ILanguageRepository, LanguageRepository>();
            container.RegisterType<ILanguagePhraseRepository, LanguagePhraseRepository>();
            container.RegisterType<IDataPhraseRepository, DataPhraseRepository>();
            container.RegisterType<IApplicationSettingRepository, ApplicationSettingRepository>();
            container.RegisterType<INotificationTemplateRepository, NotificationTemplateRepository>();            
        }
    }
}
