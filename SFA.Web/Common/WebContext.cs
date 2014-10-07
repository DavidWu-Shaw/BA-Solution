using System;
using System.Collections.Generic;
using System.Net;
using Framework.Notification.Senders;
using Framework.UoW;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Core;
using Setup.Data;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;

namespace SFA.Web.Common
{
    public class WebContext
    {
        // Static Interface
        private static volatile WebContext _instance = new WebContext();
        public static WebContext Current
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("The WebContext is not initialized");
                }

                return _instance;
            }
        }

        public ApplicationOption ApplicationOption { get; set; }
        public Dictionary<object, DomainSetting> DomainSettingList { get; set; }
        public Dictionary<object, LanguageDto> LanguageDic { get; set; }
        public Dictionary<string, LanguageDto> LanguageDicByCulture { get; set; }
        public LanguageDto DefaultLanguage { get; set; }
        public EmailSender EmailSender { get; set; }
        public NotificationTemplateData ContactUsTemplate { get; set; }
        public NotificationTemplateData WelcomeTemplate { get; set; }
        public NotificationTemplateData ResetPasswordTemplate { get; set; }
        public NotificationTemplateData OrderConfirmTemplate { get; set; }
        public NotificationTemplateData QuoteCreationTemplate { get; set; }
        public NotificationTemplateData HomeContentTemplate { get; set; }
        public NotificationTemplateData HelpContentTemplate { get; set; }

        public Dictionary<string, SubjectDto> SubjectList { get; set; }

        // Instance Interface
        private WebContext()
        {
        }

        public void Initilize(IUnitOfWork uow)
        {
            SubjectFacade subjectFacade = new SubjectFacade(uow);
            SubjectList = subjectFacade.GetSubjectList();

            DomainFacade domainFacade = new DomainFacade(uow);
            DomainSettingList = domainFacade.GetDomainSettingList();

            ApplicationSettingFacade settingFacade = new ApplicationSettingFacade(uow);
            ApplicationOption = settingFacade.GetApplicationOption();

            LanguageFacade languageFacade = new LanguageFacade(uow);
            LanguageDic = languageFacade.RetrievePublishedLanguagesAndPhrases(ApplicationOption.IsMultiLanguageSupported);
            LanguageDicByCulture = new Dictionary<string, LanguageDto>();
            foreach (var language in LanguageDic.Values)
            {
                LanguageDicByCulture.Add(language.Culture, language);
            }

            DefaultLanguage = LanguageDic[ApplicationOption.DefaultLanguageId];

            SetupEmailSender(ApplicationOption);

            SetupNotificationtemplate(uow);
        }

        public static string GetLocalizedData(string keyFormatString, object id, object languageId, string defaultValue)
        {
            string result = defaultValue;

            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && languageId != null)
            {
                string key = string.Format(keyFormatString, id);
                LanguageDto language = WebContext.Current.LanguageDic[languageId];
                if (language.DataPhrases.ContainsKey(key))
                {
                    result = language.DataPhrases[key];
                }
            }
            return result;
        }

        private void SetupEmailSender(ApplicationOption appOption)
        {
            string smtpServer = appOption.SMTPServer.Trim();
            if (smtpServer.Length > 0)
            {
                EmailSender sender = new EmailSender(smtpServer);
                sender.Port = appOption.SMTPPort;
                if (appOption.SMTPUsername.TrimHasValue() && appOption.SMTPPassword.TrimHasValue())
                {
                    sender.Credentials = new NetworkCredential(appOption.SMTPUsername.Trim(), appOption.SMTPPassword.Trim(), smtpServer);
                }

                EmailSender = sender;
            }
        }

        private void SetupNotificationtemplate(IUnitOfWork uow)
        {
            NotificationTemplateFacade facade = new NotificationTemplateFacade(uow);
            ContactUsTemplate = facade.RetrieveTemplateByType(NotificationTypes.ContactUs);
            OrderConfirmTemplate = facade.RetrieveTemplateByType(NotificationTypes.OrderConfirm);
            WelcomeTemplate = facade.RetrieveTemplateByType(NotificationTypes.Welcome);
            ResetPasswordTemplate = facade.RetrieveTemplateByType(NotificationTypes.ResetPassword);
            QuoteCreationTemplate = facade.RetrieveTemplateByType(NotificationTypes.QuoteCreation);
            HomeContentTemplate = facade.RetrieveTemplateByType(NotificationTypes.Home);
            HelpContentTemplate = facade.RetrieveTemplateByType(NotificationTypes.Help);
        }
    }
}