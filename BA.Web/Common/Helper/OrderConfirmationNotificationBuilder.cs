using System;
using System.Collections.Generic;
using System.Text;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.Notification;
using Setup.Data;

namespace BA.Web.Common.Helper
{
    internal class OrderConfirmationNotificationBuilder : NotificationBuilder
    {
        private const string OrderNumberFlag = "{{OrderNumber}}";
        private const string OrderTrackingUrlFlag = "{{OrderTrackingUrl}}";

        private string UserEmail { get; set; }
        private IEnumerable<OrderInfoDto> Orders { get; set; }
        private NotificationTemplateData Template { get; set; }

        public OrderConfirmationNotificationBuilder(string email, IEnumerable<OrderInfoDto> orders, NotificationTemplateData template)
        {
            ArgumentValidator.IsNotNull("orders", orders);
            Orders = orders;
            UserEmail = email;
            Template = template;
        }

        protected override void ComposeSubject(Notification notification)
        {
            notification.Subject = Template.Subject;
        }

        protected override void ComposeBody(Notification notification)
        {
            notification.IsBodyHtml = true;
            StringBuilder body = new StringBuilder();
            foreach (OrderInfoDto order in Orders)
            {
                string url = string.Format(WebContext.Current.ApplicationOption.OrderTrackingUrlFormat, order.OrderNumber);
                string orderBody = Template.Body;
                orderBody = orderBody.Replace(OrderNumberFlag, order.OrderNumber).Replace(OrderTrackingUrlFlag, url);
                body.Append(orderBody);
                body.Append("<br />");
                body.Append("<br />");
            }

            notification.Body = body.ToString();
        }

        protected override void ComposeTo(Notification notification)
        {
            if (IsValidEmail(UserEmail))
            {
                NotificationAddress to = new NotificationAddress(UserEmail);
                notification.To.Add(to);
            }
            // order coordinator of the website
            if (IsValidEmail(WebContext.Current.ApplicationOption.OrderCoordinatorEmail))
            {
                NotificationAddress to = new NotificationAddress(WebContext.Current.ApplicationOption.OrderCoordinatorEmail);
                notification.To.Add(to);
            }
            // customer shipto email
            if (WebContext.Current.ApplicationOption.IsEmailToCustomer)
            {
                foreach (OrderInfoDto order in Orders)
                {
                    if (IsValidEmail(order.NotificationEmail))
                    {
                        NotificationAddress to = new NotificationAddress(order.NotificationEmail);
                        notification.To.Add(to);
                    }
                }
            }
        }
    }
}
