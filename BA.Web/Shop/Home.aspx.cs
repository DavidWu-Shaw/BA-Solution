using System;
using BA.Web.Common;
using System.Web;

namespace BA.Web.Shop
{
    public partial class Home : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/Home.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.Home.PageName", "Home");

            if (Request.IsSecureConnection)
            {

            }

            Literal1.Text = WebContext.Current.HomeContentTemplate.Body;
        }
    }
}