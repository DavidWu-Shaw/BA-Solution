using System;
using BA.Web.Common;

namespace BA.Web.WebPages
{
    public partial class WarningPage : AnonymousBasePage
    {
        public const string PageUrl = @"/WebPages/WarningPage.aspx";

        public const string QryWarningMessage = "WarningMessage";

        protected WarningMessageType CurrentWarningType
        {
            get
            {
                try
                {
                    return (WarningMessageType)Enum.Parse(typeof(WarningMessageType), Request.QueryString[QryWarningMessage]);
                }
                catch
                {
                    return WarningMessageType.Unknown;

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Warning Page";

            switch (CurrentWarningType)
            {
                case WarningMessageType.NotAuthorized:
                    lblWarning.Text = Localize("WebPages.WarningPage.lblNotAuthorized", "You are not authorized to access this page.");
                    break;
                case WarningMessageType.SessionTimeout:
                    lblWarning.Text = Localize("WebPages.WarningPage.lblSessionTimeout", "Session timeout. Please restart browser.");
                    break;
                case WarningMessageType.NullObjectID:
                    lblWarning.Text = Localize("WebPages.WarningPage.lblNullObjectID", "Request object ID cannot be null.");
                    break;
                default:
                    lblWarning.Text = Localize("WebPages.WarningPage.lblException", "Unknown Exception.");
                    break;            
            }
        }
    }

}