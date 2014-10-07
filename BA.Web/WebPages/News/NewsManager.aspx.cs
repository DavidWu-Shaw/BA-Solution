using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;

namespace BA.Web.WebPages.News
{
    public partial class NewsManager : SetupBasePage
    {
        public const string QryNewsId = "NewsId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryNewsId].TryToParse<int>();
            }
        }

        private NewsDto _currentInstance;
        private NewsDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as NewsDto;
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
                NewsFacade facade = new NewsFacade(uow);
                CurrentInstance = facade.RetrieveOrNewNews(InstanceId, new NewsConverter());
            }
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.AllowModify = CurrentUserContext.IsAdmin;
            ucIDetail.CurrentInstance = CurrentInstance;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
        }

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
        }
    }
}