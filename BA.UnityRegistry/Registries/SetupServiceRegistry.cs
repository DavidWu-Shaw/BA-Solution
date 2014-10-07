using Microsoft.Practices.Unity;
using Framework.Unity;
using Setup.Service;
using Setup.Service.Contract;

namespace BA.UnityRegistry
{
    public class SetupServiceRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IMainMenuService, MainMenuService>();
            container.RegisterType<IDomainService, DomainService>();
            container.RegisterType<ISetupMenuService, SetupMenuService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<ILanguagePhraseService, LanguagePhraseService>();
            container.RegisterType<IDataPhraseService, DataPhraseService>();
            container.RegisterType<IApplicationSettingService, ApplicationSettingService>();
            container.RegisterType<INotificationTemplateService, NotificationTemplateService>();
        }
    }
}
