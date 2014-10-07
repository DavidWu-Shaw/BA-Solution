using System;
using Framework.Notification;
using CRM.ShopComponent.Dto;
using Setup.Data;

namespace BA.Web.Common.Helper
{
    public class QuoteCreationNotificationBuilder : NotificationBuilder
    {
        private const string EmailFlag = "{{Email}}";
        private const string ContactPersonFlag = "{{ContactPerson}}";
        private const string PhoneFlag = "{{Phone}}";
        private const string ProductFlag = "{{Product}}";
        private const string AmountFlag = "{{Amount}}";

        private QuoteInfoDto Quote { get; set; }
        private NotificationTemplateData Template { get; set; }

        public QuoteCreationNotificationBuilder(QuoteInfoDto quote, NotificationTemplateData template)
        {
            Quote = quote;
            Template = template;
        }

        protected override void ComposeSubject(Notification notification)
        {
            notification.Subject = Template.Subject.Replace(ProductFlag, Quote.ProductName);
        }

        protected override void ComposeBody(Notification notification)
        {
            notification.IsBodyHtml = true;

            string body = Template.Body;
            body = body.Replace(EmailFlag, Quote.Email)
                        .Replace(ContactPersonFlag, Quote.ContactPerson)
                        .Replace(PhoneFlag, Quote.Phone)
                        .Replace(AmountFlag, Quote.Amount.ToString())
                        .Replace(ProductFlag, Quote.ProductName);

            notification.Body = body.ToString();
        }

        protected override void ComposeTo(Notification notification)
        {
            // order coordinator of the website
            if (IsValidEmail(WebContext.Current.ApplicationOption.QuoteCoordinatorEmail))
            {
                NotificationAddress to = new NotificationAddress(WebContext.Current.ApplicationOption.QuoteCoordinatorEmail);
                notification.To.Add(to);
            }
        }
    }
}