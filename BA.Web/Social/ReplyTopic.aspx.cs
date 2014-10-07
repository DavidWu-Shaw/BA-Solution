using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using Telerik.Web.UI;

namespace BA.Web.Social
{
    public partial class ReplyTopic : AppBasePage
    {
        public const string PageUrl = @"/Social/ReplyTopic.aspx";
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

        private string ReturnUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.ReplyTopic.PageName", "Reply Topic");
            IsNarrowPage = true;

            InitControls();

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
            }
        }

        private void LoadTopic()
        {
            lblTitle.Text = string.Format("{0}: {1}", Localize("Social.ReplyTopic.Reply", "Reply"), CurrentInstance.Title);
        }

        private void InitControls()
        {
            edContent.ToolsFile = ServerPath + "/Styles/BasicTools.xml";
            edContent.EditModes = Telerik.Web.UI.EditModes.Design;
            EditorModule moduleStatistics = new EditorModule();
            moduleStatistics.Name = "RadEditorStatistics";
            edContent.Modules.Add(moduleStatistics);

            string url = string.Format("{0}?{1}={2}", TopicPage.PageUrl, TopicPage.QryInstanceId, InstanceId);
            ReturnUrl = GetUrl(url);
            btnCancel.PostBackUrl = ReturnUrl;
        }

        protected override bool CheckPagePermission()
        {
            return !CurrentUserContext.IsAnonymous;
        }

        private void LoadPosts()
        {
            ucPostList.ShowReply = false;
            ucPostList.Posts = CurrentInstance.Posts;
            ucPostList.LoadData();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            string content = edContent.Content.Trim();
            if (content.Length > 0)
            {
                PostInfoDto post = new PostInfoDto();
                post.TopicId = CurrentInstance.TopicId;
                post.Content = content;
                post.IssuedById = CurrentUserContext.User.UserId;

                IFacadeUpdateResult<PostData> result = SavePost(post);
                if (result.IsSuccessful)
                {
                    Response.Redirect(ReturnUrl, true);
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private IFacadeUpdateResult<PostData> SavePost(PostInfoDto post)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                PostFacade facade = new PostFacade(uow);
                IFacadeUpdateResult<PostData> result = facade.SavePost(post);
                return result;
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnPost.Text = Localize("Social.ReplyTopic.btnPost", "Post");
            btnCancel.Text = Localize("Social.ReplyTopic.btnCancel", "Cancel");
        }
    }
}