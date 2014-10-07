using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BA.Core;
using BA.Web.Common;
using BA.Web.Shop;
using BA.Web.WebPages;
using Setup.Component.Dto;
using Setup.Core;

namespace BA.Web.MasterPages
{
    public partial class SiteMaster : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLanguageMenu();
                LoadMainMenu();
            }

            base.OnPreRender(e);

            DisplayHeader();
        }

        private void DisplayHeader()
        {
            if (IsNarrowPage)
            {
                divPage.Attributes.Add("class", "Npage");
            }

            lblAppName.Text = WebContext.Current.ApplicationOption.Name;
            if (!CurrentUserContext.IsAnonymous)
            {
                lblUser.Text = CurrentUserContext.User.Username;
                string lastConnectDate = string.Empty;
                if (CurrentUserContext.User.LastConnectDate.HasValue)
                {
                    lastConnectDate = ", Last visit at " + CurrentUserContext.User.LastConnectDate.Value.ToString(WebContext.Current.ApplicationOption.DateTimeFormat);
                }
                lblUser.ToolTip = string.Format("{0}{1}", CurrentUserContext.User.Email, lastConnectDate);
                hlSetup.Text = Localize("MasterPages.SiteMaster.Setup", "My Account");
                hlSetup.NavigateUrl = ServerPath + PersonalInfoPage.PageUrl;
            }
            else
            {
                hlSetup.Visible = false;
                spSetup.Visible = false;
            }
            if (WebContext.Current.ApplicationOption.IsShoppingSupported)
            {
                hlMyCart.Text = Localize("MasterPages.SiteMaster.MyCart", "View Cart");
                hlMyCart.NavigateUrl = ServerPath + ShoppingCart.PageUrl;
            }
            else
            {
                hlMyCart.Visible = false;
                spMyCart.Visible = false;
            }

            if (CurrentUserContext.IsAnonymous)
            {
                hlLogout.Text = Localize("MasterPages.SiteMaster.SignIn", "[Sign in]");
                hlLogout.NavigateUrl = string.Format("{0}?{1}={2}", ServerPath + Logon.PageUrl, Logon.QryReturnUrl, System.Web.HttpUtility.UrlEncode(Request.RawUrl));
            }
            else
            {
                hlLogout.Text = Localize("MasterPages.SiteMaster.SignOut", "[Sign out]");
                hlLogout.NavigateUrl = string.Format("{0}?{1}={2}", ServerPath + Logon.PageUrl, Logon.QryLogout, Logon.QryLogout);
            }

            hlAbout.Text = Localize("MasterPages.SiteMaster.hlAbout", "About Us");
            hlAbout.NavigateUrl = ServerPath + Home.PageUrl;

            hlContact.Text = Localize("MasterPages.SiteMaster.hlContact", "Contact Us");
            hlContact.NavigateUrl = ServerPath + ContactUsPage.PageUrl;

            hlHelp.Text = Localize("MasterPages.SiteMaster.hlHelp", "Help Center");
            hlHelp.NavigateUrl = ServerPath + HelpCenter.PageUrl;
        }

        private void LoadLanguageMenu()
        {
            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported)
            {
                LangMenu.Items.Clear();
                foreach (LanguageDto item in WebContext.Current.LanguageList.Values)
                {
                    MenuItem menu = new MenuItem();
                    LangMenu.Items.Add(menu);
                    menu.Value = item.StringId;
                    menu.Text = item.Label;
                }
            }
        }

        private void LoadMainMenu()
        {
            IEnumerable<MainMenuDto> accessableMainMenus = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].MainMenus;

            if (accessableMainMenus != null)
            {
                NavMenu.Items.Clear();
                foreach (MainMenuDto item in accessableMainMenus)
                {
                    MenuItem menu = new MenuItem();
                    NavMenu.Items.Add(menu);
                    menu.Value = item.StringId;
                    menu.NavigateUrl = ServerPath + item.NavigateUrl;
                    menu.Text = WebContext.GetLocalizedData(SetupConst.MainMenuTextKeyFormatString, item.Id, CurrentUserContext.CurrentLanguage.Id, item.MenuText);
                }
            }
        }

        protected void LangMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            int languageId = Convert.ToInt32(e.Item.Value);
            CurrentUserContext.SetCurrentLanguage(languageId);

            // Redirect page in order to change language
            Response.Redirect(Request.RawUrl, false);
        }
    }
}
