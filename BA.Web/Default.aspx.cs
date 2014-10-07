using System;
using BA.Web.Common;

namespace BA.Web
{
	public partial class Default : AnonymousBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string defaultUrl = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].DefaultUrl;
			if (defaultUrl != null && defaultUrl.TrimHasValue())
			{
				Response.Redirect(ServerPath + defaultUrl);
			}
		}
	}
}