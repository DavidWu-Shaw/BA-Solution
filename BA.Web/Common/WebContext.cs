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

namespace BA.Web.Common
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
        public Dictionary<object, LanguageDto> LanguageList { get; set; }
        public EmailSender EmailSender { get; set; }
        public NotificationTemplateData ContactUsTemplate { get; set; }
        public NotificationTemplateData WelcomeTemplate { get; set; }
        public NotificationTemplateData ResetPasswordTemplate { get; set; }
        public NotificationTemplateData OrderConfirmTemplate { get; set; }
        public NotificationTemplateData QuoteCreationTemplate { get; set; }
        public NotificationTemplateData HomeContentTemplate { get; set; }
        public NotificationTemplateData HelpContentTemplate { get; set; }
        
        private Dictionary<string, SubjectDto> SubjectList { get; set; }

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
            LanguageList = languageFacade.RetrievePublishedLanguagesAndPhrases(ApplicationOption.IsMultiLanguageSupported);

            SetupEmailSender(ApplicationOption);

            SetupNotificationtemplate(uow);
        }

        public SubjectDto GetSubject(string subjectType)
        {
            SubjectDto subject = SubjectList[subjectType];
            //if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && languageId != null)
            //{
            //    // Implement multi-language for Subject
            //    if (subject.SubjectLanguagesDic.ContainsKey(languageId))
            //    {
            //        SubjectLanguageDto subjectLanguage = subject.SubjectLanguagesDic[languageId];
            //        subject.SubjectLabel = subjectLanguage.SubjectLabel;
            //    }

            //    foreach (SubjectFieldDto subjectField in subject.SubjectFields)
            //    {
            //        // Implement multi-language for Subject Field
            //        if (subjectField.SubjectFieldLanguagesDic.ContainsKey(languageId))
            //        {
            //            SubjectFieldLanguageDto subjectFieldLanguage = subjectField.SubjectFieldLanguagesDic[languageId];
            //            subjectField.FieldLabel = subjectFieldLanguage.FieldLabel;
            //            subjectField.HelpText = subjectFieldLanguage.HelpText;
            //        }
            //    }
            //    foreach (SubjectChildListDto childList in subject.SubjectChildLists)
            //    {
            //        // Implement multi-language for Subject ChildList
            //        if (childList.SubjectChildListLanguagesDic.ContainsKey(languageId))
            //        {
            //            SubjectChildListLanguageDto childListLanguage = childList.SubjectChildListLanguagesDic[languageId];
            //            childList.Title = childListLanguage.Title;
            //        }
            //    }
            //}

            return subject;
        }

        public static string GetLocalizedData(string keyFormatString, object id, object languageId, string defaultValue)
        {
            string result = defaultValue;

            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && languageId != null)
            {
                string key = string.Format(keyFormatString, id);
                LanguageDto language = WebContext.Current.LanguageList[languageId];
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
