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

namespace BA.Web.WebPages.Activity
{
    public partial class ActivityEdit : AppBasePage
    {
        public const string QryActivityId = "ActivityId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryActivityId].TryToParse<int>();
            }
        }

        private ActivityDto _currentInstance;
        private ActivityDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            ActivityFacade facade = new ActivityFacade(uow);
                            _currentInstance = facade.RetrieveOrNewActivity(InstanceId, new ActivityConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as ActivityDto;
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
            InitControl();
        }

        protected override bool CheckPagePermission()
        {
            if (CurrentUserContext.IsAdmin || (CurrentUserContext.IsEmployee && (CurrentInstance.IsNew || object.Equals(CurrentInstance.EmployeeId, CurrentUserContext.User.MatchId))))
            {
                return true;
            }
            return false;
        }

        private void InitControl()
        {
            ucIEdit.InstanceNewing += new InstanceNewingEventHandler(ucIEdit_InstanceNewing);
            ucIEdit.InstanceSaving += new InstanceSavingEventHandler(ucIEdit_InstanceSaving);

            if (CurrentUserContext.IsEmployee)
            {
                ucIEdit.ControlledFieldName = ucIEdit.CurrentSubject.MasterSubjectIdField;
                ucIEdit.ControlledFieldValue = CurrentUserContext.User.MatchId;
            }

            ucIEdit.CurrentInstance = CurrentInstance;
            PageName = ucIEdit.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ActivityFacade facade = new ActivityFacade(uow);
                ActivityDto instance = facade.RetrieveOrNewActivity(InstanceId, new ActivityConverter());
                CurrentInstance = instance;
                ucIEdit.CurrentInstance = instance;
            }
        }

        protected void ucIEdit_InstanceNewing(object sender, InstanceNewingEventArgs e)
        {
            RetrieveData();
        }

        protected void ucIEdit_InstanceSaving(object sender, InstanceSavingEventArgs e)
        {
            ActivityDto instance = e.Instance as ActivityDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    ActivityFacade facade = new ActivityFacade(uow);
                    IFacadeUpdateResult<ActivityData> result = facade.SaveActivity(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<ActivityDto>(new ActivityConverter());
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