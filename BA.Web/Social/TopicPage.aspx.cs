using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Social
{
    public partial class TopicPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Social/TopicPage.aspx";
        public const string QryInstanceId = "TopicId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryInstanceId].TryToParse<int>();
            }
        }

        private TopicBriefInfoDto _currentInstance;
        private TopicBriefInfoDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as TopicBriefInfoDto;
                    }
                }

                return _currentInstance;
            }
            set
            {
                _currentInstance = value;
                SaveInSession(_currentInstance, InstanceStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.TopicPage.PageName", "Topic Detail");

            if (!IsPostBack)
            {
                RetrieveData();
                LoadTopic();
                LoadPosts();
            }
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                TopicFacade facade = new TopicFacade(uow);
                CurrentInstance = facade.RetrieveTopicInfo(InstanceId, new TopicBriefInfoConverter());
                // Get Posts of current topic
                PostFacade postFacade = new PostFacade(uow);
                CurrentInstance.Posts = postFacade.RetrievePostsByTopic(InstanceId, new PostInfoConverter());

                IFacadeUpdateResult<TopicData> result = facade.UpdateTopicVisits(InstanceId);
            }
        }

        private void LoadTopic()
        {
            lblTitle.Text = CurrentInstance.Title;
        }

        private void LoadPosts()
        {
            string url = string.Format("{0}?{1}={2}", ReplyTopic.PageUrl, ReplyTopic.QryInstanceId, CurrentInstance.TopicId);
            ucPostList.ReplyPostBackUrl = GetUrl(url);
            ucPostList.ShowReply = !CurrentInstance.DenyReply;
            ucPostList.EnableReply = !CurrentUserContext.IsAnonymous;
            ucPostList.Posts = CurrentInstance.Posts;
            ucPostList.LoadData();
        }
    }
}