using System;
using Framework.Notification;

namespace BA.Web.Common.Helper
{
    public class NotificationBuilder : INotificationBuilder
    {
        public virtual Notification Build()
        {
            Notification notification = new Notification();

            ComposeSubject(notification);
            ComposeBody(notification);
            ComposeTo(notification);
            ComposeFrom(notification);

            return notification;
        }

        protected virtual void ComposeSubject(Notification notification)
        {

        }

        protected virtual void ComposeBody(Notification notification)
        {

        }

        protected virtual void ComposeTo(Notification notification)
        {
            // site coordinator of the website
            if (IsValidEmail(WebContext.Current.ApplicationOption.SiteCoordinatorEmail))
            {
                NotificationAddress to = new NotificationAddress(WebContext.Current.ApplicationOption.SiteCoordinatorEmail);
                notification.To.Add(to);
            }
        }

        protected virtual void ComposeFrom(Notification notification)
        {
            if (IsValidEmail(WebContext.Current.ApplicationOption.SMTPUsername))
            {
                notification.From = new NotificationAddress(WebContext.Current.ApplicationOption.SMTPUsername);
            }
            else
            {
                notification.From = new NotificationAddress(WebContext.Current.ApplicationOption.DefaultEmailFrom);
            }
        }

        protected bool IsValidEmail(string email)
        {
            if (email.TrimHasValue())
            {
                return true;
            }

            return false;
        }
    }
}