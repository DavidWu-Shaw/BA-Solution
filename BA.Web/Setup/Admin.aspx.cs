using System;
using BA.Web.Common;

namespace BA.Web.Setup
{
    public partial class Admin : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/Admin.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Admin";
        }
    }
}