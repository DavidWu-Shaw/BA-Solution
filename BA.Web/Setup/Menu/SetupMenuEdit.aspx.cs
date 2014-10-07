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

namespace BA.Web.Setup.Menu
{
    public partial class SetupMenuEdit : AdminSetupBasePage
    {
        public const string QrySetupMenuId = "SetupMenuId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QrySetupMenuId].TryToParse<int>();
            }
        }

        private SetupMenuDto _currentInstance;
        private SetupMenuDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            SetupMenuFacade facade = new SetupMenuFacade(uow);
                            _currentInstance = facade.RetrieveOrNewSetupMenu(InstanceId);
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as SetupMenuDto;
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
                SetupMenuFacade facade = new SetupMenuFacade(uow);
                SetupMenuDto instance = facade.RetrieveOrNewSetupMenu(InstanceId);
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
            SetupMenuDto instance = e.Instance as SetupMenuDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    SetupMenuFacade facade = new SetupMenuFacade(uow);
                    IFacadeUpdateResult<SetupMenuData> result = facade.SaveSetupMenu(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<SetupMenuDto>(new SetupMenuConverter());
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