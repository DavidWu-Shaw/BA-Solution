using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BA.Web
{
    public partial class GeneralErrorPage : System.Web.UI.Page
    {
        public const string PageUrl = @"/GeneralErrorPage.aspx";
        public const string QryErroredPageUrl = "aspxerrorpath";

        private string ErroredPageUrl
        {
            get
            {
                return Request.QueryString[QryErroredPageUrl];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "General Error Page";

            DisplayError();
        }

        private void DisplayError()
        {
            lblWarning.Text = "Unexpected error Occured. ";

            Exception ex = HttpContext.Current.Server.GetLastError();
            if (ex != null)
            {
                Exception innerEx = ex.InnerException;
                if (innerEx != null)
                {
                    lblWarning.Text += innerEx.ToString();
                }
            }
        }
    }
}