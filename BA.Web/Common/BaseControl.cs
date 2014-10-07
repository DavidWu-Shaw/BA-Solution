using System;
using System.Collections.Generic;
using Framework.Globalization;
using Web.Helpers;

namespace BA.Web.Common
{
    public class BaseControl : System.Web.UI.UserControl
    {
        protected const string InstanceStateKey = "InstanceSessionKey";

        protected BasePage BasePage
        {
            get
            {
                return base.Page as BasePage;
            }
        }

        protected string ServerPath
        {
            get
            {
                return BasePage.ServerPath;
            }
        }

        protected UserContext CurrentUserContext
        {
            get { return BasePage.CurrentUserContext; }
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ImplementLabelLocalization();
        }

        protected virtual void ImplementLabelLocalization()
        {
        }

        protected string Localize(string key, string defaultValue)
        {
            return StringLocalizer.Localize(key, CurrentUserContext.CurrentLanguage.Id, defaultValue);
        }

        // Javascript methods
        protected void RegisterScriptResource<ControlType>()
        {
            JavaScriptHelper.RegisterScriptResource<ControlType>(Page);
        }

        protected void RegisterScriptResource(Type controlType, string jsNamespace, string jsName)
        {
            JavaScriptHelper.RegisterScriptResource(Page, controlType, jsNamespace, jsName);
        }

        // Serialization methods
        protected void SaveInSession(object obj, string sessionIndex)
        {
            SessionHelper.SaveInSession(obj, sessionIndex);
        }

        protected object GetFromSession(string sessionIndex)
        {
            return SessionHelper.GetFromSession(sessionIndex);
        }

        protected bool IsInSession(string sessionIndex)
        {
            return SessionHelper.IsInSession(sessionIndex);
        }

        protected void RemoveFromSession(string sessionIndex)
        {
            SessionHelper.RemoveFromSession(sessionIndex);
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

        public void AlertMessages(string strMsg)
        {
            BasePage.AlertMessages(strMsg);
        }

        protected string GetUrl(string url)
        {
            string fullUrl = ServerPath + url;
            return fullUrl;
        }

        protected string GetUrl(string urlFormatString, object subjectInstanceID)
        {
            string fullUrl = ServerPath + string.Format(urlFormatString, subjectInstanceID);
            return fullUrl;
        }
    }
}