using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BA.Web.Common;
using Setup.Component.Dto;
using BA.Core;
using Setup.Core;

namespace BA.Web.MasterPages
{
    public partial class SetupMasterPage : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMaster.CurrentUserContext = CurrentUserContext;

            LoadSetupMenu();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            SiteMaster.ContentPageName = ContentPageName;
            SiteMaster.IsNarrowPage = IsNarrowPage;

            lblTitle.Text = Localize("MasterPages.lblTitle", "Setup");
        }

        private void LoadSetupMenu()
        {
            IEnumerable<SetupMenuDto> accessableMenus = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].SetupMenus;

            if (accessableMenus != null)
            {
                foreach (SetupMenuDto item in accessableMenus)
                {
                    MenuItem menu = new MenuItem();
                    SetupMenu.Items.Add(menu);
                    menu.Value = item.StringId;
                    menu.NavigateUrl = ServerPath + item.NavigateUrl;
                    menu.Text = WebContext.GetLocalizedData(SetupConst.SetupMenuTextKeyFormatString, item.Id, CurrentUserContext.CurrentLanguage.Id, item.MenuText);
                }
            }
        }
    }
}