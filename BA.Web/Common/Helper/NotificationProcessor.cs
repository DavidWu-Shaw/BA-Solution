using System;
using System.Collections.Generic;
using System.Net;
using CRM.ShopComponent.Dto;
using Framework.Notification;
using Framework.Notification.Senders;

namespace BA.Web.Common.Helper
{
    public static class NotificationProcessor
    {
        public static void SendOrderConfirmation(string email, IEnumerable<OrderInfoDto> orders)
        {
            if (WebContext.Current.EmailSender != null)
            {
                NotificationFactory factory = new NotificationFactory(WebContext.Current.EmailSender);
                OrderConfirmationNotificationBuilder builder = new OrderConfirmationNotificationBuilder(email, orders, WebContext.Current.OrderConfirmTemplate);
                factory.Send(builder);
            }
        }

        public static void SendContactUs(ContactUsInfoDto contactUsInfo)
        {
            if (WebContext.Current.EmailSender != null)
            {
                NotificationFactory factory = new NotificationFactory(WebContext.Current.EmailSender);
                ContactUsNotificationBuilder builder = new ContactUsNotificationBuilder(contactUsInfo, WebContext.Current.ContactUsTemplate);
                factory.Send(builder);
            }
        }

        public static void SendQuoteCreation(QuoteInfoDto quote)
        {
            if (WebContext.Current.EmailSender != null)
            {
                NotificationFactory factory = new NotificationFactory(WebContext.Current.EmailSender);
                QuoteCreationNotificationBuilder builder = new QuoteCreationNotificationBuilder(quote, WebContext.Current.QuoteCreationTemplate);
                factory.Send(builder);
            }
        }
    }
}
