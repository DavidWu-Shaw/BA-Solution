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

namespace BA.Web.WebPages.Employee
{
    public partial class EmployeeEdit : SetupBasePage
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
            if (CurrentUserContext.IsEmployee && object.Equals(CurrentInstance.Id, CurrentUserContext.User.MatchId) || CurrentUserContext.IsAdmin)
            {
                return true;
            }
            return false;
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
                EmployeeFacade facade = new EmployeeFacade(uow);
                EmployeeDto instance = facade.RetrieveOrNewEmployee(InstanceId, new EmployeeConverter());
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
            EmployeeDto instance = e.Instance as EmployeeDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    EmployeeFacade facade = new EmployeeFacade(uow);
                    IFacadeUpdateResult<EmployeeData> result = facade.SaveEmployee(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<EmployeeDto>(new EmployeeConverter());
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