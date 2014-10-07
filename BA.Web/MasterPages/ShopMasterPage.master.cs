using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BA.Web.MasterPages
{
    public partial class ShopMasterPage : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMaster.CurrentUserContext = CurrentUserContext;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            SiteMaster.ContentPageName = ContentPageName;
            SiteMaster.IsNarrowPage = IsNarrowPage;
        }
    }
}