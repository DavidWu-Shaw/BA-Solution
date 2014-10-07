using System;
using BA.Web.Common;
using BA.Web.Common.Helper;

namespace BA.Web.WebPages.Dashboard
{
    public partial class MyDashboard : AppBasePage
    {
        public const string PageUrl = @"/WebPages/Dashboard/MyDashboard.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Dashboard";
            ucSchedulerControl.DataProvider = new MySchedulerProvider(CurrentUserContext);
        }
    }
}