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

namespace BA.Web.WebPages.Customer
{
    public partial class CustomerManager : AppBasePage
    {
        public const string QryCustomerId = "CustomerId";

        private int? _instanceId;
        private int? InstanceId
        {
            get
            {
                if (_instanceId == null)
                {
                    _instanceId = Request.QueryString[QryCustomerId].TryToParse<int>();
                }
                return _instanceId;
            }
        }

        private CustomerDto _currentInstance;
        private CustomerDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            CustomerFacade facade = new CustomerFacade(uow);
                            _currentInstance = facade.RetrieveOrNewCustomer(InstanceId, new CustomerConverter());

                            // Retrive Contacts belong to Customer
                            ContactFacade contactFacade = new ContactFacade(uow);
                            _currentInstance.Contacts = contactFacade.RetrieveContactsByCustomer(InstanceId, new ContactConverter());

                            // Retrieve ScheduleEvents of Customer
                            ScheduleEventFacade scheduleEventFacade = new ScheduleEventFacade(uow);
                            _currentInstance.ScheduleEvents = scheduleEventFacade.RetrieveScheduleEventsByCustomer(InstanceId, new ScheduleEventConverter());

                            // Activity
                            ActivityFacade activityFacade = new ActivityFacade(uow);
                            _currentInstance.Activitys = activityFacade.RetrieveActivitysByCustomer(InstanceId, new ActivityConverter());

                            // Order
                            OrderFacade orderFacade = new OrderFacade(uow);
                            _currentInstance.Orders = orderFacade.RetrieveOrdersByCustomer(InstanceId, new OrderConverter());

                            // Transaction
                            TransactionFacade TransactionFacade = new TransactionFacade(uow);
                            _currentInstance.Transactions = TransactionFacade.RetrieveTransactionsByCustomer(InstanceId, new TransactionConverter());

                            // ShipTo
                            ShipToFacade shipToFacade = new ShipToFacade(uow);
                            _currentInstance.ShipTos = shipToFacade.RetrieveShipTosByCustomer(InstanceId, new ShipToConverter());
                        }

                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as CustomerDto;
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
            if (CurrentUserContext.IsCustomer && object.Equals(CurrentInstance.Id, CurrentUserContext.User.MatchId))
            {
                return true;
            }
            return base.CheckPagePermission();
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.AllowModify = CurrentUserContext.IsEmployee || CurrentUserContext.IsAdmin;
            ucIDetail.CurrentInstance = CurrentInstance;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Contact:
                    e.Instances = CurrentInstance.Contacts;
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
                case InstanceTypes.Order:
                    e.Instances = CurrentInstance.Orders;
                    break;
                case InstanceTypes.ShipTo:
                    e.Instances = CurrentInstance.ShipTos;
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
                case InstanceTypes.Contact:
                    e.Instance = new ContactDto();
                    break;
                case InstanceTypes.ScheduleEvent:
                    e.Instance = new ScheduleEventDto();
                    break;
                case InstanceTypes.ShipTo:
                    e.Instance = new ShipToDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.Contact:
                        ContactFacade facade = new ContactFacade(uow);
                        ContactDto contactDto = e.Instance as ContactDto;
                        contactDto.CustomerId = CurrentInstance.Id;
                        // Save data
                        IFacadeUpdateResult<ContactData> result = facade.SaveContact(contactDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Contacts = facade.RetrieveContactsByCustomer(CurrentInstance.Id, new ContactConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                    case InstanceTypes.ScheduleEvent:
                        ScheduleEventFacade facade2 = new ScheduleEventFacade(uow);
                        ScheduleEventDto eventDto = e.Instance as ScheduleEventDto;
                        eventDto.ObjectId = CurrentInstance.Id;
                        eventDto.ObjectTypeId = (int)EventObjectTypes.Customer;
                        // Save data
                        IFacadeUpdateResult<ScheduleEventData> result2 = facade2.SaveScheduleEvent(eventDto);
                        e.IsSuccessful = result2.IsSuccessful;
                        if (result2.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.ScheduleEvents = facade2.RetrieveScheduleEventsByCustomer(CurrentInstance.Id, new ScheduleEventConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result2.ValidationResult, result2.Exception);
                        }
                        break;
                    case InstanceTypes.ShipTo:
                        ShipToFacade facade3 = new ShipToFacade(uow);
                        ShipToDto shipToDto = e.Instance as ShipToDto;
                        shipToDto.CustomerId = CurrentInstance.Id;
                        // Save data
                        IFacadeUpdateResult<ShipToData> result3 = facade3.SaveShipTo(shipToDto);
                        e.IsSuccessful = result3.IsSuccessful;
                        if (result3.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.ShipTos = facade3.RetrieveShipTosByCustomer(CurrentInstance.Id, new ShipToConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result3.ValidationResult, result3.Exception);
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
                    case InstanceTypes.Contact:
                        ContactFacade facade = new ContactFacade(uow);
                        IFacadeUpdateResult<ContactData> result = facade.DeleteContact(e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Contacts = facade.RetrieveContactsByCustomer(CurrentInstance.Id, new ContactConverter());
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
                            CurrentInstance.ScheduleEvents = facade2.RetrieveScheduleEventsByCustomer(CurrentInstance.Id, new ScheduleEventConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result2.ValidationResult, result2.Exception);
                        }
                        break;
                    case InstanceTypes.ShipTo:
                        ShipToFacade facade3 = new ShipToFacade(uow);
                        // Delete data
                        IFacadeUpdateResult<ShipToData> result3 = facade3.DeleteShipTo(e.Instance.Id);
                        e.IsSuccessful = result3.IsSuccessful;
                        if (result3.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.ShipTos = facade3.RetrieveShipTosByCustomer(CurrentInstance.Id, new ShipToConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result3.ValidationResult, result3.Exception);
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
