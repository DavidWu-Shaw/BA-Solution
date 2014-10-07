using BA.Web.Common;
using Framework.Globalization;

namespace BA.Web.MasterPages
{
    public class MasterPageBase : System.Web.UI.MasterPage
    {
        public const string ServerPath = "~";

        public string ContentPageName { get; set; }
        public bool IsNarrowPage { get; set; }
        public UserContext CurrentUserContext { get; set; }

        public BA.Web.MasterPages.SiteMaster SiteMaster
        {
            get
            {
                return (BA.Web.MasterPages.SiteMaster)(base.Master);
            }
        }

        protected string Localize(string key, string defaultValue)
        {
            return StringLocalizer.Localize(key, CurrentUserContext.CurrentLanguage.Id, defaultValue);
        }
    }
}