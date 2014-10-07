using System;
using BA.Web.Common;
using Telerik.Web.UI;

namespace BA.Web.MasterPages
{
    public partial class AppMasterPage : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMaster.CurrentUserContext = CurrentUserContext;

            if (!IsPostBack)
            {
                if (WebContext.Current.ApplicationOption.EnableNotification)
                {
                    RadNotification1.UpdateInterval = 3000;
                }
                else
                {
                    RadNotification1.UpdateInterval = 0;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            SiteMaster.ContentPageName = ContentPageName;
            SiteMaster.IsNarrowPage = IsNarrowPage;
        }

        protected void RadNotification1_CallbackUpdate(object sender, RadNotificationEventArgs e)
        {
            int newMsgs = DateTime.Now.Second % 10;
            //simulates cases when there are and there are not new messages
            if (newMsgs == 5 || newMsgs == 7 || newMsgs == 8 || newMsgs == 9)
            {
                RadNotification1.Value = "";
            }
            else
            {
                RadNotification1.Value = newMsgs.ToString();
            }

            lblNotification.Text = string.Format("You have {0} messages.", newMsgs);
        }
    }
}