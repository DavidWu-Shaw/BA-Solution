using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.UoW;
using BA.Web.Converters;

namespace BA.Web.WebPages.Employee
{
    public partial class EmployeeManager : SetupBasePage
    {
        public const string QryEmployeeId = "EmployeeId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryEmployeeId].TryToParse<int>();
            }
        }

        private EmployeeDto _currentInstance;
        private EmployeeDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            EmployeeFacade facade = new EmployeeFacade(uow);
                            _currentInstance = facade.RetrieveOrNewEmployee(InstanceId, new EmployeeConverter());

                            // Activity
                            ActivityFacade activityFacade = new ActivityFacade(uow);
                            _currentInstance.Activitys = activityFacade.RetrieveActivitysByEmployee(InstanceId, new ActivityConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as EmployeeDto;
                    }
                }

                return _currentInstance;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        protected override bool CheckPagePermission()
        {
            if (CurrentUserContext.IsEmployee && object.Equals(CurrentInstance.Id, CurrentUserContext.User.MatchId) || CurrentUserContext.IsAdmin)
            {
                return true;
            }
            return false;
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.CurrentInstance = CurrentInstance;
            ucIDetail.AllowModify = CurrentUserContext.IsAdmin || CurrentUserContext.IsEmployee;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Activity:
                    e.Instances = CurrentInstance.Activitys;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Activity:
                    e.Instance = new ActivityDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.Activity:
                        ActivityFacade facade = new ActivityFacade(uow);
                        ActivityDto instanceDto = e.Instance as ActivityDto;
                        instanceDto.EmployeeId = CurrentInstance.Id;
                        // Save data
                        IFacadeUpdateResult<ActivityData> result = facade.SaveActivity(instanceDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Activitys = facade.RetrieveActivitysByEmployee(CurrentInstance.Id, new ActivityConverter());
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
                    case InstanceTypes.Activity:
                        ActivityFacade facade = new ActivityFacade(uow);
                        IFacadeUpdateResult<ActivityData> result = facade.DeleteActivity(e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Activitys = facade.RetrieveActivitysByEmployee(CurrentInstance.Id, new ActivityConverter());
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