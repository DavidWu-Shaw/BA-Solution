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

namespace BA.Web.WebPages.Contact
{
    public partial class ContactManager : AppBasePage
    {
        public const string QryContactId = "ContactId";

        private int? _instanceId;
        private int? InstanceId
        {
            get
            {
                if (_instanceId == null)
                {
                    _instanceId = Request.QueryString[QryContactId].TryToParse<int>();
                }
                return _instanceId;
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
                            // Retrieve ScheduleEvents of Customer
                            ScheduleEventFacade scheduleEventFacade = new ScheduleEventFacade(uow);
                            _currentInstance.ScheduleEvents = scheduleEventFacade.RetrieveScheduleEventsByContact(InstanceId, new ScheduleEventConverter());
                            // Activity
                            ActivityFacade activityFacade = new ActivityFacade(uow);
                            _currentInstance.Activitys = activityFacade.RetrieveActivitysByContact(InstanceId, new ActivityConverter());
                            // Transaction
                            TransactionFacade transactionFacade = new TransactionFacade(uow);
                            _currentInstance.Transactions = transactionFacade.RetrieveTransactionsByContact(InstanceId, new TransactionConverter());

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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        protected override bool CheckPagePermission()
        {
            if (CurrentUserContext.IsEmployee && object.Equals(CurrentInstance.EmployeeId, CurrentUserContext.User.MatchId))
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
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.AllowModify = CurrentUserContext.IsEmployee;
            ucIDetail.CurrentInstance = CurrentInstance;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.ContactContactMethod:
                    e.Instances = CurrentInstance.ContactContactMethods;
                    break;
                case InstanceTypes.ScheduleEvent:
                    e.Instances = CurrentInstance.ScheduleEvents;
                    break;
                case InstanceTypes.Activity:
                    e.Instances = CurrentInstance.Activitys;
                    break;
                case InstanceTypes.Transaction:
                    e.Instances = CurrentInstance.Transactions;
                    break;
                default:
                    e.Instances = null;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.ContactContactMethod:
                    e.Instance = new ContactContactMethodDto();
                    break;
                case InstanceTypes.ScheduleEvent:
                    ScheduleEventDto instance = new ScheduleEventDto();
                    instance.StartDate = DateTime.Today;
                    e.Instance = instance;
                    break;
                case InstanceTypes.Activity:
                    e.Instance = new ActivityDto();
                    break;
                case InstanceTypes.Transaction:
                    e.Instance = new TransactionDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.ContactContactMethod:
                        ContactFacade facade = new ContactFacade(uow);
                        ContactContactMethodDto contactMethodDto = e.Instance as ContactContactMethodDto;
                        // Save data
                        IFacadeUpdateResult<ContactData> result = facade.SaveContactContactMethod(CurrentInstance.Id, contactMethodDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh 
                            ContactDto savedCurrentInstance = result.ToDto(new ContactConverter());
                            CurrentInstance.ContactContactMethods = savedCurrentInstance.ContactContactMethods;
                            // Note: Can't refresh CurrentInstance this way, it will ruin other child list data
                            //ucIDetail.CurrentInstance = savedInstance;
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;

                    case InstanceTypes.ScheduleEvent:
                        ScheduleEventFacade facade2 = new ScheduleEventFacade(uow);
                        ScheduleEventDto eventDto = e.Instance as ScheduleEventDto;
                        eventDto.ObjectId = CurrentInstance.Id;
                        eventDto.ObjectTypeId = (int)EventObjectTypes.Contact;
                        // Save data
                        IFacadeUpdateResult<ScheduleEventData> result2 = facade2.SaveScheduleEvent(eventDto);
                        e.IsSuccessful = result2.IsSuccessful;
                        if (result2.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.ScheduleEvents = facade2.RetrieveScheduleEventsByContact(CurrentInstance.Id, new ScheduleEventConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result2.ValidationResult, result2.Exception);
                        }
                        break;
                    case InstanceTypes.Activity:
                        ActivityFacade facade3 = new ActivityFacade(uow);
                        ActivityDto activityDto = e.Instance as ActivityDto;
                        activityDto.ContactId = CurrentInstance.Id;
                        activityDto.CustomerId = CurrentInstance.CustomerId;
                        activityDto.EmployeeId = CurrentUserContext.User.MatchId;
                        // Save data
                        IFacadeUpdateResult<ActivityData> result3 = facade3.SaveActivity(activityDto);
                        e.IsSuccessful = result3.IsSuccessful;
                        if (result3.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Activitys = facade3.RetrieveActivitysByContact(CurrentInstance.Id, new ActivityConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result3.ValidationResult, result3.Exception);
                        }
                        break;
                    case InstanceTypes.Transaction:
                        TransactionFacade facade4 = new TransactionFacade(uow);
                        TransactionDto TransactionDto = e.Instance as TransactionDto;
                        TransactionDto.ContactId = CurrentInstance.Id;
                        TransactionDto.CustomerId = CurrentInstance.CustomerId;
                        // Save data
                        IFacadeUpdateResult<TransactionData> result4 = facade4.SaveTransaction(TransactionDto);
                        e.IsSuccessful = result4.IsSuccessful;
                        if (result4.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Transactions = facade4.RetrieveTransactionsByContact(CurrentInstance.Id, new TransactionConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result4.ValidationResult, result4.Exception);
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
                    case InstanceTypes.ContactContactMethod:
                        ContactFacade facade = new ContactFacade(uow);
                        IFacadeUpdateResult<ContactData> result = facade.DeleteContactContactMethod(CurrentInstance.Id, e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh whole list
                            ContactDto savedCurrentInstance = result.ToDto(new ContactConverter());
                            CurrentInstance.ContactContactMethods = savedCurrentInstance.ContactContactMethods;
                            // Note: Can't refresh CurrentInstance this way, it will ruin other child list data
                            //ucIDetail.CurrentInstance = savedCurrentInstance;
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                    case InstanceTypes.ScheduleEvent:
                        ScheduleEventFacade facade2 = new ScheduleEventFacade(uow);
                        // Delete data
                        IFacadeUpdateResult<ScheduleEventData> result2 = facade2.DeleteScheduleEvent(e.Instance.Id);
                        e.IsSuccessful = result2.IsSuccessful;
                        if (result2.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.ScheduleEvents = facade2.RetrieveScheduleEventsByContact(CurrentInstance.Id, new ScheduleEventConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result2.ValidationResult, result2.Exception);
                        }
                        break;
                    case InstanceTypes.Activity:
                        ActivityFacade facade3 = new ActivityFacade(uow);
                        // Delete data
                        IFacadeUpdateResult<ActivityData> result3 = facade3.DeleteActivity(e.Instance.Id);
                        e.IsSuccessful = result3.IsSuccessful;
                        if (result3.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Activitys = facade3.RetrieveActivitysByContact(CurrentInstance.Id, new ActivityConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result3.ValidationResult, result3.Exception);
                        }
                        break;
                    case InstanceTypes.Transaction:
                        TransactionFacade facade4 = new TransactionFacade(uow);
                        // Delete data
                        IFacadeUpdateResult<TransactionData> result4 = facade4.DeleteTransaction(e.Instance.Id);
                        e.IsSuccessful = result4.IsSuccessful;
                        if (result4.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Transactions = facade4.RetrieveTransactionsByContact(CurrentInstance.Id, new TransactionConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result4.ValidationResult, result4.Exception);
                        }
                        break;
                }
            }
        }
    }
}
