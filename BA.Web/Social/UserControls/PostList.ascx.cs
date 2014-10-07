using System;
using System.Collections.Generic;
using BA.Web.Common;
using CRM.ShopComponent.Dto;

namespace BA.Web.Social.UserControls
{
    public partial class PostList : BaseControl
    {
        public bool ShowReply { get; set; }
        public bool EnableReply { get; set; }
        public string ReplyPostBackUrl { get; set; }
        public IEnumerable<PostInfoDto> Posts { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            rptItems.DataSource = Posts;
            rptItems.DataBind();
        }

        public string GetPostedTime(object obj)
        {
            PostInfoDto item = (PostInfoDto)obj;
            return string.Format("{0} {1}", TextOfPostedOn, item.IssuedTime.ToString(WebContext.Current.ApplicationOption.DateTimeFormat));
        }

        public bool GetReplyVisibility(object obj)
        {
            return ShowReply;
        }

        public bool GetReplyEnable(object obj)
        {
            return EnableReply;
        }

        public string GetPostBackUrl(object obj)
        {
            return ReplyPostBackUrl;
        }

        public string GetTextOfReply()
        {
            return TextOfLink;
        }

        private string _textOfLink;
        private string TextOfLink
        {
            get
            {
                if (_textOfLink == null)
                {
                    _textOfLink = Localize("Social.UserControls.PostList.btnReply", "Reply");
                }
                return _textOfLink;
            }
        }

        private string _textOfPostedOn;
        private string TextOfPostedOn
        {
            get
            {
                if (_textOfPostedOn == null)
                {
                    _textOfPostedOn = Localize("Social.UserControls.PostList.PostedOn", "Posted on");
                }
                return _textOfPostedOn;
            }
        }
    }
}