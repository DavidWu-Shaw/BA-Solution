using System;
using Framework.UoW;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using BA.Web.Common;
using Framework.Component;
using CRM.Data;
using BA.Web.Converters;
using BA.Web.Common.Helper;

namespace BA.Web.WebPages.Contact
{
    public partial class ContactEdit : AppBasePage
    {
        public const string QryContactId = "ContactId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryContactId].TryToParse<int>();
            }
        }

        private ContactDto _currentInstance;
        private ContactDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            ContactFacade facade = new ContactFacade(uow);
                            _currentInstance = facade.RetrieveOrNewContact(InstanceId, new ContactConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as ContactDto;
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
            if (CurrentUserContext.IsEmployee && (CurrentInstance.IsNew || object.Equals(CurrentInstance.EmployeeId, CurrentUserContext.User.MatchId)))
            {
                return true;
            }
            else
            {
                return CurrentUserContext.IsAdmin;
            }
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
                ContactFacade facade = new ContactFacade(uow);
                ContactDto instance = facade.RetrieveOrNewContact(InstanceId, new ContactConverter());
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
            ContactDto instance = e.Instance as ContactDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    ContactFacade facade = new ContactFacade(uow);
                    IFacadeUpdateResult<ContactData> result = facade.SaveContact(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<ContactDto>(new ContactConverter());
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