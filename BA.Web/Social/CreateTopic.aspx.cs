using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using Telerik.Web.UI;

namespace BA.Web.Social
{
    public partial class CreateTopic : AppBasePage
    {
        public const string PageUrl = @"/Social/CreateTopic.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.CreateTopic.PageName", "Forum");
            IsNarrowPage = true;

            if (!IsPostBack)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            edContent.ToolsFile = ServerPath + "/Styles/BasicTools.xml";
            edContent.EditModes = Telerik.Web.UI.EditModes.Design;
            EditorModule moduleStatistics = new EditorModule();
            moduleStatistics.Name = "RadEditorStatistics";
            edContent.Modules.Add(moduleStatistics);

            btnCancel.PostBackUrl = GetUrl(TopicList.PageUrl);
        }

        protected override bool CheckPagePermission()
        {
            return !CurrentUserContext.IsAnonymous;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string content = edContent.Content.Trim();
            if (title.Length > 0 && content.Length > 0)
            {
                TopicInfoDto topic = new TopicInfoDto();
                topic.IssuedById = CurrentUserContext.User.UserId;
                topic.Title = title;

                PostInfoDto post = new PostInfoDto();
                post.Content = content;
                post.IssuedById = CurrentUserContext.User.UserId;

                IFacadeUpdateResult<TopicData> result = SaveCreatedTopic(topic, post);
                if (result.IsSuccessful)
                {
                    Response.Redirect(GetUrl(TopicList.PageUrl), true);
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private IFacadeUpdateResult<TopicData> SaveCreatedTopic(TopicInfoDto topic, PostInfoDto post)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                TopicFacade facade = new TopicFacade(uow);
                IFacadeUpdateResult<TopicData> result = facade.CreateTopic(topic, post);
                return result;
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblTitle.Text = Localize("Social.CreateTopic.lblTitle", "New Topic");
            txtTitle.EmptyMessage = Localize("Social.CreateTopic.txtEmpty", "Enter title here");
            btnPost.Text = Localize("Social.CreateTopic.btnPost", "Post");
            btnCancel.Text = Localize("Social.CreateTopic.btnCancel", "Cancel");
        }
    }
}