using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;

namespace BA.Web.WebPages.News
{
    public partial class NewsEdit : SetupBasePage
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

        private void InitControl()
        {
            ucIEdit.InstanceNewing += new InstanceNewingEventHandler(ucIEdit_InstanceNewing);
            ucIEdit.InstanceSaving += new InstanceSavingEventHandler(ucIEdit_InstanceSaving);

            ucIEdit.CurrentInstance = CurrentInstance;
            PageName = ucIEdit.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                NewsFacade facade = new NewsFacade(uow);
                NewsDto instance = facade.RetrieveOrNewNews(InstanceId, new NewsConverter());
                if (instance.IsNew)
                {
                    instance.IssuedById = CurrentUserContext.User.UserId;
                }
                CurrentInstance = instance;
            }
        }

        protected void ucIEdit_InstanceNewing(object sender, InstanceNewingEventArgs e)
        {
            RetrieveData();
            ucIEdit.CurrentInstance = CurrentInstance;
        }

        protected void ucIEdit_InstanceSaving(object sender, InstanceSavingEventArgs e)
        {
            NewsDto instance = e.Instance as NewsDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    NewsFacade facade = new NewsFacade(uow);
                    IFacadeUpdateResult<NewsData> result = facade.SaveNews(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<NewsDto>(new NewsConverter());
                    }
                    else
                    {
                        // Deal with Update result
                        ProcUpdateResult(result.ValidationResult, result.Exception);
                    }
                }
            }
        }
    }
}