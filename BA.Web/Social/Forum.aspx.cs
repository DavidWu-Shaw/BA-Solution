using System;
using BA.Web.Common;

namespace BA.Web.Social
{
    public partial class Forum : AnonymousBasePage
    {
        public const string PageUrl = @"/Social/Forum.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.Forum.PageName", "Forum");

            Response.Redirect(GetUrl(TopicList.PageUrl));
        }
    }
}