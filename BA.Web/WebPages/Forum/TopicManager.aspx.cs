using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.UoW;

namespace BA.Web.WebPages.Forum
{
    public partial class TopicManager : SetupBasePage
    {
        public const string QryTopicId = "TopicId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryTopicId].TryToParse<int>();
            }
        }

        private TopicDto _currentInstance;
        private TopicDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as TopicDto;
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
            if (!IsPostBack)
            {
                RetrieveData();
            }

            InitControl();
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                TopicFacade facade = new TopicFacade(uow);
                CurrentInstance = facade.RetrieveOrNewTopic(InstanceId, new TopicConverter());

                PostFacade postFacade = new PostFacade(uow);
                CurrentInstance.Posts = postFacade.RetrievePostsByTopic(InstanceId, new PostConverter());
            }
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.CurrentInstance = CurrentInstance;
            ucIDetail.AllowModify = CurrentUserContext.IsAdmin;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Post:
                    e.Instances = CurrentInstance.Posts;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Post:
                    PostDto post = new PostDto();
                    post.IssuedTime = DateTime.Now;
                    e.Instance = post;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.Post:
                        PostFacade facade = new PostFacade(uow);
                        PostDto instanceDto = e.Instance as PostDto;
                        instanceDto.TopicId = CurrentInstance.Id;
                        // Save data
                        IFacadeUpdateResult<PostData> result = facade.SavePost(instanceDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Posts = facade.RetrievePostsByTopic(CurrentInstance.Id, new PostConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                }
            }
        }

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.Post:
                        PostFacade facade = new PostFacade(uow);
                        IFacadeUpdateResult<PostData> result = facade.DeletePost(e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Posts = facade.RetrievePostsByTopic(CurrentInstance.Id, new PostConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                }
            }
        }

        private void SetButtonAttribute()
        {
            if (InstanceId.HasValue)
            {
                //ucIDetail.EditPostBackUrl = GetPageUrl(SDApplicationPage.ProjectEdit, new SDPageParameter(ProjectEdit.QryProjectID, ProjectId));
            }
        }
    }
}