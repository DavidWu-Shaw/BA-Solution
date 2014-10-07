using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Social
{
    public partial class TopicList : AnonymousBasePage
    {
        public const string PageUrl = @"/Social/TopicList.aspx";

        private IEnumerable<TopicInfoDto> Topics { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.TopicList.PageName", "Topic List");
            InitControls();

            if (!IsPostBack)
            {
                RetrieveData();
                LoadData();
            }
        }

        private void InitControls()
        {
            if (CurrentUserContext.IsAnonymous)
            {
                btnNew.Enabled = false;
            }
            else
            {
                btnNew.Enabled = true;
                btnNew.PostBackUrl = GetUrl(CreateTopic.PageUrl);
            }
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                TopicFacade facade = new TopicFacade(uow);
                Topics = facade.RetrieveAllTopic(new TopicInfoConverter());
            }
        }

        private void LoadData()
        {
            rptItems.DataSource = Topics;
            rptItems.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }

        public bool GetImgVisibility(object obj)
        {
            TopicInfoDto item = (TopicInfoDto)obj;
            return item.IsSticky;
        }

        public string GetStartedBy(object obj)
        {
            TopicInfoDto item = (TopicInfoDto)obj;
            return string.Format("{0} {1}", TextOfStartedBy, item.IssuedBy);
        }

        private string _textOfStartedBy;
        private string TextOfStartedBy
        {
            get
            {
                if (_textOfStartedBy == null)
                {
                    _textOfStartedBy = Localize("Social.UserControls.TopicList.StartedBy", "Started by");
                }
                return _textOfStartedBy;
            }
        }

        public string GetLastPostBy(object obj)
        {
            TopicInfoDto item = (TopicInfoDto)obj;
            return string.Format("by {0}", item.LastPostBy);
        }

        public string GetLastPostTime(object obj)
        {
            TopicInfoDto item = (TopicInfoDto)obj;
            return item.LastPostTime.ToString(WebContext.Current.ApplicationOption.DateTimeFormat);
        }

        public string GetNavigateUrl(object obj)
        {
            TopicInfoDto item = (TopicInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", TopicPage.PageUrl, TopicPage.QryInstanceId, item.TopicId);
            return GetUrl(url);
        }

        public string GetHeaderOfPost()
        {
            return Localize("Social.TopicList.lblPosts", "Posts");
        }

        public string GetHeaderOfVisit()
        {
            return Localize("Social.TopicList.lblVisits", "Visits");
        }

        public string GetHeaderOfLastPost()
        {
            return Localize("Social.TopicList.lblLastPost", "Last Post");
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            txtKey.EmptyMessage = Localize("Social.TopicList.txtEmpty", "Search this forum");
            btnSearch.Text = Localize("Social.TopicList.btnSearch", "Search");
            btnNew.Text = Localize("Social.TopicList.btnNew", "New Topic");
        }
    }
}