using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;

namespace BA.Web.Social
{
    public partial class NewsList : AnonymousBasePage
    {
        public const string PageUrl = @"/Social/NewsList.aspx";

        private IEnumerable<NewsDto> News { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.NewsList.PageName", "News");

            if (!IsPostBack)
            {
                RetrieveData();
                LoadData();
            }
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                NewsFacade facade = new NewsFacade(uow);
                News = facade.RetrieveAllNews(new NewsConverter());
            }
        }

        private void LoadData()
        {
            rptNews.DataSource = News;
            rptNews.DataBind();
        }

        public string GetNavigateUrl(object obj)
        {
            NewsDto item = (NewsDto)obj;
            string url = string.Format("{0}?{1}={2}", NewsPage.PageUrl, NewsPage.QryInstanceId, item.Id);
            return GetUrl(url);
        }

        public string GetPostedTime(object obj)
        {
            NewsDto item = (NewsDto)obj;
            return item.IssuedTime.ToString(WebContext.Current.ApplicationOption.DateTimeFormat);
        }

        public string GetContent(object obj)
        {
            NewsDto item = (NewsDto)obj;
            if (item.Content.Length < WebContext.Current.ApplicationOption.NewsContentBriefLength)
            {
                return item.Content;
            }
            else
            {
                return item.Content.Substring(0, WebContext.Current.ApplicationOption.NewsContentBriefLength);
            }
        }

        public bool GetButtonVisibility(object obj)
        {
            NewsDto item = (NewsDto)obj;
            if (item.Content.Length > WebContext.Current.ApplicationOption.NewsContentBriefLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetTextOfLink()
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
                    _textOfLink = Localize("Social.NewsList.lnkReadMore", "Read more ...");
                }
                return _textOfLink;
            }
        }
    }
}