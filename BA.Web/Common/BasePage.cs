using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using BA.Web.MasterPages;
using BA.Web.WebPages;
using Framework.Globalization;
using Framework.Validation;
using Setup.Component;
using Web.Helpers;

namespace BA.Web.Common
{
    public class BasePage : System.Web.UI.Page
    {
        public const int EmptyIntValue = -1;
        protected const string InstanceStateKey = "InstanceSessionKey";
        private const string PopUpCancel = "JavaScript: window.close(); return false";
        
        protected string PageName { get; set; }
        protected bool IsNarrowPage { get; set; }
        protected bool AllowAnonymous { get; set; }
        protected bool RequireSSL { get; set; }

        public string ServerPath
        {
            get
            {
                return "~";
            }
        }

        public MasterPageBase MasterPage
        {
            get
            {
                return (MasterPageBase)(base.Master);
            }
        }

        private string UserLoginUrl
        {
            get
            {
                return string.Format("{0}?{1}={2}&{3}={4}", ServerPath + Logon.PageUrl, Logon.QryLogout, Logon.TimeoutLogout, Logon.QryReturnUrl, System.Web.HttpUtility.UrlEncode(Request.RawUrl));
            }
        }

        private UserContext _currentUserContext;
        public UserContext CurrentUserContext
        {
            get
            {
                if (_currentUserContext == null)
                {
                    if (HttpContext.Current.Session[UserContext.UserContextStateKey] != null)
                    {
                        _currentUserContext = (UserContext)HttpContext.Current.Session[UserContext.UserContextStateKey];
                    }
                    else
                    {
                        _currentUserContext = new UserContext(new UserIdentity());
                        HttpContext.Current.Session[UserContext.UserContextStateKey] = _currentUserContext;
                    }
                }

                if (_currentUserContext.IsAnonymous && !AllowAnonymous)
                {
                    // Redirect immediately
                    Response.Redirect(UserLoginUrl, true);
                }

                return _currentUserContext;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                RemoveFromSession(InstanceStateKey);
            }

            base.OnPreInit(e);

            JavaScriptHelper.ClearLists();
        }

        private void PushSSL()
        {
            const string SECURE = "https://";
            const string UNSECURE = "http://";

            //Force required into secure channel
            if (RequireSSL && Request.IsSecureConnection == false)
            {
                Response.Redirect(Request.Url.ToString().Replace(UNSECURE, SECURE));
            }
            //Force non-required out of secure channel
            if (!RequireSSL && Request.IsSecureConnection == true)
            {
                Response.Redirect(Request.Url.ToString().Replace(SECURE, UNSECURE));
            }
        }  

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must 
                    // have timed out (can't use the cookie collection because even on first 
                    // request it already contains the cookie (request and response
                    // seem to share the collection)
                    string szCookieHeader = Request.Headers["Cookie"];
                    if ((!IsCallback) && (null != szCookieHeader) && (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        string pageUrl = string.Format("{0}?{1}={2}", ServerPath + WarningPage.PageUrl, WarningPage.QryWarningMessage, WarningMessageType.SessionTimeout);
                        Response.Redirect(pageUrl, true);
                    }
                }
            }

            if (WebContext.Current.ApplicationOption.EnableSSL)
            {
                PushSSL();
            }

            bool allowed = CheckPagePermission();

            if (!allowed)
            {
                // Revoke access
                RedirectToWarningPage(WarningMessageType.NotAuthorized);
            }

            MasterPage.CurrentUserContext = CurrentUserContext;
        }

        protected virtual bool CheckPagePermission()
        {
            return true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            Title = string.Format("{0} - {1}", PageName, WebContext.Current.ApplicationOption.Name);
            MasterPage.ContentPageName = PageName;
            MasterPage.IsNarrowPage = IsNarrowPage;

            base.OnPreRender(e);
            LoadJsClientScripts();

            ImplementLabelLocalization();
        }

        protected override void InitializeCulture()
        {
            if (CurrentUserContext.CurrentCultureInfo != null)
            {
                //Thread.CurrentThread.CurrentCulture = CurrentUserContext.CurrentCultureInfo;
                Thread.CurrentThread.CurrentUICulture = CurrentUserContext.CurrentCultureInfo;
            }
            base.InitializeCulture();
        }

        protected virtual void ImplementLabelLocalization()
        {
        }

        protected string Localize(string key, string defaultValue)
        {
            return StringLocalizer.Localize(key, CurrentUserContext.CurrentLanguage.Id, defaultValue);
        }

        public virtual int GetQueryStringValueInt(string valueName)
        {
            try
            {
                // if it is -1, it means not obtain query integer from client ( browser)
                string queryStringValue = this.GetQueryStringValue(valueName) as string;
                if (string.IsNullOrEmpty(queryStringValue))
                {
                    return EmptyIntValue;
                }
                else
                {
                    return Convert.ToInt32(queryStringValue);
                }
            }
            catch { return EmptyIntValue; }
        }

        public virtual long GetQueryStringValueLong(string valueName)
        {
            try
            {
                // if it is -1, it means not obtain query integer from client ( browser)
                string queryStringValue = this.GetQueryStringValue(valueName) as string;
                if (string.IsNullOrEmpty(queryStringValue))
                {
                    return EmptyIntValue;
                }
                else
                {
                    return Convert.ToInt64(queryStringValue);
                }
            }
            catch { return EmptyIntValue; }
        }

        public virtual string GetQueryStringValueString(string valueName)
        {
            object queryStringValue = this.GetQueryStringValue(valueName);
            if (queryStringValue == null)
            {
                return String.Empty;
            }
            else
            {
                return queryStringValue.ToString();
            }
        }

        public virtual object GetQueryStringValue(string valueName)
        {
            object queryStringValue = Request.QueryString[valueName];
            return queryStringValue;
        }

        protected Dictionary<string, string> JsClientSideVariables
        {
            get
            {
                return JavaScriptHelper.JsClientSideVariables;
            }
        }

        protected Dictionary<string, string> JsClientScripts
        {
            get
            {
                return JavaScriptHelper.JsClientScripts;
            }
        }

        protected Dictionary<string, string> JsClientScriptsFile
        {
            get
            {
                return JavaScriptHelper.JsClientScriptsFile;
            }
        }

        // Javascript function
        private void LoadJsClientScripts()
        {
            JavaScriptHelper.LoadJsClientScripts(this, JsClientScriptsFile, JsClientScripts, JsClientSideVariables);
        }

        protected void RegisterScriptResource<ControlType>()
        {
            JavaScriptHelper.RegisterScriptResource<ControlType>(this);
        }

        protected void RegisterScriptResource(Type controlType, string jsNamespace, string jsName)
        {
            JavaScriptHelper.RegisterScriptResource(this, controlType, jsNamespace, jsName);
        }

        // Session functions
        protected void SaveInSession(object obj, string sessionKey)
        {
            SessionHelper.SaveInSession(obj, sessionKey);
        }

        protected object GetFromSession(string sessionKey)
        {
            return SessionHelper.GetFromSession(sessionKey);
        }

        protected void RemoveFromSession(string sessionKey)
        {
            SessionHelper.RemoveFromSession(sessionKey);
        }

        protected bool IsInSession(string sessionKey)
        {
            return SessionHelper.IsInSession(sessionKey);
        }

        // ViewState methods
        protected void SaveInViewState(object obj, string key)
        {
            ViewState[key] = obj;
        }

        protected object GetFromViewState(string key)
        {
            return ViewState[key];
        }

        protected void RemoveFromViewState(string key)
        {
            if (IsInViewState(key))
            {
                ViewState.Remove(key);
            }
        }

        protected bool IsInViewState(string key)
        {
            if (ViewState != null && ViewState[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ProcUpdateResult(ValidationResult validationResult, Exception exception)
        {
            if (exception != null)
            {
                throw exception;
            }
            else
            {
                StringBuilder msg = new StringBuilder();
                foreach (ValidationItem item in validationResult.Items)
                {
                    msg.AppendFormat("{0}: {1}\\n", item.PropertyName, item.Message);
                }

                AlertMessages(msg.ToString());
            }
        }

        protected void ProcException(Exception ex, string alertMsg)
        {
            StringBuilder msg = new StringBuilder(ex.Message);
            Exception innerEx = ex.InnerException;
            while (innerEx != null)
            {
                msg.Append(innerEx.Message + "\\n");
                innerEx = innerEx.InnerException;
            }
            // send email with ex

            // alert to user
            AlertMessages(string.Format("{0}\\n{1}", alertMsg, msg.ToString()));
        }

        public void AlertMessages(string strMsg)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(@"<script type='text/javascript'>");
            strBuilder.Append(@"    alert('" + strMsg.Replace("'", "\\'").Replace("\"", "\\'") + "');");
            strBuilder.Append(@"</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), "ApplicationAlertScriptKey", strBuilder.ToString());
        }

        protected void RegisterSciptForDisableButonOnClick(WebControl aControl)
        {
            aControl.Attributes.Add("onclick", string.Format("javascript: {0}.disabled=true; {1};", aControl.ClientID, this.Page.ClientScript.GetPostBackEventReference(aControl, null)));
        }

        protected void RegisterSciptForClickToClose(WebControl aCancelButton)
        {
            aCancelButton.Attributes.Add("onclick", PopUpCancel);
        }

        protected string GetUrl(string url)
        {
            string fullUrl = ServerPath + url;
            return fullUrl;
        }

        protected void RedirectToWarningPage(WarningMessageType message)
        {
            string url = string.Format("{0}?{1}={2}", ServerPath + WarningPage.PageUrl, WarningPage.QryWarningMessage, WarningMessageType.NotAuthorized.ToString());
            Response.Redirect(url, true);
        }
    }
}