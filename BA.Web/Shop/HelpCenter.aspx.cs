using System;
using BA.Web.Common;
using System.Web;

namespace BA.Web.Shop
{
    public partial class HelpCenter : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/HelpCenter.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.HelpCenter.PageName", "Help Center");
            Literal1.Text = WebContext.Current.HelpContentTemplate.Body;
        }
    }
}