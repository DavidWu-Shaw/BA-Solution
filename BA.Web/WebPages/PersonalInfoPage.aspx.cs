using System;
using BA.Web.Common;
using Setup.Component.Dto;
using Framework.UoW;
using BA.UnityRegistry;
using Setup.Component;
using Telerik.Web.UI;
using Framework.Component;
using Setup.Data;

namespace BA.Web.WebPages
{
    public partial class PersonalInfoPage : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/PersonalInfoPage.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("WebPages.PersonalInfoPage", "Personal Information");
            IsNarrowPage = true;

            if (!IsPostBack)
            {
                LoadDDL();
                LoadData();
            }
        }

        private void SaveChanges()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                UserFacade facade = new UserFacade(uow);
                UserDto user = facade.RetrieveOrNewUser(CurrentUserContext.User.UserId);

                user.Username = txtUsername.Text.Trim();
                user.LanguageId = ddlLanguage.SelectedValue;
                user.ModifiedDate = DateTime.Now;

                IFacadeUpdateResult<UserData> result = facade.SaveUser(user);
                if (result.IsSuccessful)
                {
                    CurrentUserContext.User.Username = user.Username;
                    CurrentUserContext.User.LanguageId = user.LanguageId;
                    lbResultMsg.Text = "Save changes done. Please sign in again to take affect.";
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private void LoadDDL()
        {
            foreach(DomainSetting domain in WebContext.Current.DomainSettingList.Values)
            {
                ddlDomain.Items.Add(new RadComboBoxItem(domain.Name, domain.DomainId.ToString()));
            }

            foreach (LanguageDto language in WebContext.Current.LanguageList.Values)
            {
                ddlLanguage.Items.Add(new RadComboBoxItem(language.Name, language.StringId));
            }
        }

        private void LoadData()
        {
            if (CurrentUserContext != null && CurrentUserContext.User != null)
            {
                txtEmail.Text = CurrentUserContext.User.Email;
                txtUsername.Text = CurrentUserContext.User.Username;
                ddlLanguage.SelectedValue = CurrentUserContext.User.LanguageId.ToString();
                if (CurrentUserContext.User.LastConnectDate.HasValue)
                {
                    txtLastTime.Text = CurrentUserContext.User.LastConnectDate.Value.ToString(WebContext.Current.ApplicationOption.DateTimeFormat);
                }
                ddlDomain.SelectedValue = CurrentUserContext.User.DomainId.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();

            lblTitle.Text = "Personal Information";
            lblEmail.Text = "Email";
            lblUsername.Text = "Username";
            lblLanguage.Text = "Default Language";
            lblDomain.Text = "User Domain";
            lblMatch.Text = "User Match";
            lblLastTime.Text = "Last Visit";
            //lblSignin.Text = Localize("Logon.lblSignin", "Sign in");
        }
    }
}