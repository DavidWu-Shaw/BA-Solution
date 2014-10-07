using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using Framework.UoW;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Component.Converters;
using Setup.Data;

namespace BA.Web.Setup.User
{
    public partial class UserEdit : AdminSetupBasePage
    {
        public const string QryUserId = "UserId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryUserId].TryToParse<int>();
            }
        }

        private UserDto _currentInstance;
        private UserDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            UserFacade facade = new UserFacade(uow);
                            _currentInstance = facade.RetrieveOrNewUser(InstanceId);
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as UserDto;
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
                UserFacade facade = new UserFacade(uow);
                UserDto instance = facade.RetrieveOrNewUser(InstanceId);
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
            UserDto instance = e.Instance as UserDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    UserFacade facade = new UserFacade(uow);
                    IFacadeUpdateResult<UserData> result = facade.SaveUser(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<UserDto>(new UserConverter());
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