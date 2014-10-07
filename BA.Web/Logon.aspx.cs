using System;
using System.Web;
using System.Web.Security;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop;
using Framework.Logging;
using Framework.Security;
using Framework.UoW;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Data;
using BA.Web.WebPages;

namespace BA.Web
{
    public partial class Logon : AnonymousBasePage
    {
        public const string PageUrl = "/Logon.aspx";

        public const string QryLoginFlag = "LoginFlag";
        public const string QryLogout = "Logout";
        public const string QryReturnUrl = "ReturnUrl";

        public const string TimeoutLogout = "Timeout";
        public const string SignOut = "Logout";

        private string LogoutReason
        {
            get { return Request.QueryString[QryLogout]; }
        }

        protected override void OnInit(EventArgs e)
        {
            RequireSSL = true;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Logon.PageName", "Sign in");

            divReg.Visible = WebContext.Current.ApplicationOption.EnableRegister;

            if (!IsPostBack)
            {
                RetriveUserInfoFromCookie();
                VerifyTimeoutOrLogout();
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblSignin.Text = Localize("Logon.lblSignin", "Sign in");
            lblTip.Text = Localize("Logon.lblTip", "Please enter your email and password.");
            btnSignin.Text = Localize("Logon.btnSignin", "Sign in");
            cbxRemember.Text = Localize("Logon.cbxRemember", "Remember me");
            hlForget.Text = Localize("Logon.hlForget", "Forgot password?");
            lblCreateA.Text = Localize("Logon.lblCreateA", "Register");
            lblCreateTip.Text = Localize("Logon.lblCreateTip", "Please fill out following form to register. ");
            btnCreate.Text = Localize("Logon.btnCreate", "Register");
            btnSubmit.Text = Localize("Logon.btnSubmit", "Submit");
            hlCancel.Text = Localize("Logon.hlCancel", "Cancel");
            lblRecover.Text = Localize("Logon.lblRecover", "Recover password");
            lblEmail.Text = Localize("Logon.Email", "Email:*");
            lblEmailCreate.Text = Localize("Logon.Email", "Email:*");
            lblEmailRecover.Text = Localize("Logon.Email", "Email:*");
            lblPasswd.Text = Localize("Logon.Passwd", "Password:*");
            lblPasswdCreate.Text = Localize("Logon.Passwd", "Password:*");
            lblRecoverTip.Text = Localize("Logon.lblRecoverTip", "Please enter the email you registered with us.");
            lblUsername.Text = Localize("Logon.lblUsername", "Username:");
        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {
            LoginToApplication();
        }

        private void LoginToApplication()
        {
            string email = txtEmail.Text.Trim();
            string password = txtPasswd.Text.Trim();

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                ProcAuthentication(email, EncryptionHelper.Encrypt(password));
            }
        }

        private void ProcAuthentication(string email, string encryptedPassword)
        {
            UserIdentity authenticatedUser = null;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                AuthenticateFacade facade = new AuthenticateFacade(uow);
                authenticatedUser = facade.Authenticate(email, encryptedPassword);
            }

            if (authenticatedUser != null)
            {
                AfterLogin(authenticatedUser);
            }
            else
            {
                FailedLogin(email);
            }
        }

        private void FailedLogin(string email)
        {
            ApplicationLog.WriteWarning("Unexpected User try to sign in Email: " + email);
            lbMsg.Text = "Invalid Email or Password, please try again.";
            lbMsg.Visible = true;
        }

        private void AfterLogin(UserIdentity user)
        {
            CurrentUserContext.Initilize(user);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(user.Email, this.cbxRemember.Checked, 24 * 60);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            RedirectAferValidate();
        }

        private void RedirectAferValidate()
        {
            string returnURL = this.Request.QueryString["ReturnUrl"];

            if (returnURL != null && returnURL.Contains(WarningPage.PageUrl))
            {
                string defaultUrl = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].DefaultUrl;
                if (defaultUrl != null && defaultUrl.TrimHasValue())
                {
                    Response.Redirect(ServerPath + defaultUrl);
                }
                else
                {
                    Response.Redirect(ServerPath + Home.PageUrl);
                }
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(CurrentUserContext.User.Email, true);
            }

            //if (userSecurity.IsCustomer)
            //{
            //    FormsAuthentication.RedirectFromLoginPage(userSecurity.User.Username, true);
            //}
            //else
            //{
            //    string defaultUrl = WebContext.Current.DomainSettingList[userSecurity.User.DomainId].DefaultUrl;
            //    if (defaultUrl.TrimHasValue())
            //    {
            //        Response.Redirect(ServerPath + defaultUrl);
            //    }
            //    else
            //    {
            //        Response.Redirect("~" + Shopping.PageUrl, true);
            //    }
            //}
        }

        private void RetriveUserInfoFromCookie()
        {
            HttpCookie enCryptCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (enCryptCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(enCryptCookie.Value);
                //ticket.is
                this.txtEmail.Text = ticket.Name;
                this.cbxRemember.Checked = ticket.IsPersistent;
            }
        }

        private void VerifyTimeoutOrLogout()
        {
            if (object.Equals(LogoutReason, QryLogout))
            {
                FormsAuthentication.SignOut();
                Session.RemoveAll();
                Response.Redirect(ServerPath + Home.PageUrl);
            }
        }

        #region Register User

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            UserDto userDto = new UserDto();
            userDto.Email = txtRegEmail.Text.Trim();
            userDto.Password = EncryptionHelper.Encrypt(txtRegPasswd.Text.Trim());
            userDto.Username = txtUsername.Text.Trim();

            RegisterUser(userDto);
        }

        private void RegisterUser(UserDto userDto)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                RegisterFacade facade = new RegisterFacade(uow);
                IFacadeUpdateResult<UserData> result = facade.RegisterUser(userDto);
                if (result.IsSuccessful)
                {
                    ClearInput();
                    lbMsg.Text = "Register successful.";
                    lbMsg.Visible = true;
                    // TODO: send notification

                    // Sign in registered user
                    ProcAuthentication(userDto.Email, userDto.Password);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private void ClearInput()
        {
            txtEmail.Text = string.Empty;
            txtPasswd.Text = string.Empty;
            txtUsername.Text = string.Empty;
        }

        #endregion Register User

        #region Recover password

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        #endregion Recover password
    }
}