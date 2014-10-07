using System;
using CRM.ShopComponent.Dto;
using Framework.Notification;
using Setup.Data;

namespace BA.Web.Common.Helper
{
    public class ContactUsNotificationBuilder : NotificationBuilder
    {
        private const string SubjectFlag = "{{Subject}}";
        private const string EmailFlag = "{{Email}}";
        private const string NameFlag = "{{Name}}";
        private const string PhoneFlag = "{{Phone}}";
        private const string AddressFlag = "{{Address}}";
        private const string MessageFlag = "{{Message}}";

        private ContactUsInfoDto ContactUsInfo { get; set; }
        private NotificationTemplateData Template { get; set; }

        public ContactUsNotificationBuilder(ContactUsInfoDto contactUsInfo, NotificationTemplateData template)
        {
            ContactUsInfo = contactUsInfo;
            Template = template;
        }

        protected override void ComposeSubject(Notification notification)
        {
            notification.Subject = Template.Subject.Replace(EmailFlag, ContactUsInfo.Email);
        }

        protected override void ComposeBody(Notification notification)
        {
            notification.IsBodyHtml = true;
            string body = Template.Body;
            body = body.Replace(EmailFlag, ContactUsInfo.Email)
                        .Replace(NameFlag, ContactUsInfo.Name)
                        .Replace(PhoneFlag, ContactUsInfo.Phone)
                        .Replace(AddressFlag, ContactUsInfo.Address)
                        .Replace(SubjectFlag, ContactUsInfo.Subject)
                        .Replace(MessageFlag, ContactUsInfo.Message);

            notification.Body = body.ToString();
        }
    }
}