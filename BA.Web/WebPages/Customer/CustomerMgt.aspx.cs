using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;
using System.Collections.Generic;
using BA.Web.Common.Helper;
using BA.Web.Converters;

namespace BA.Web.WebPages.Customer
{
    public partial class CustomerMgt : AppBasePage
    {
        public const string PageUrl = @"/WebPages/Customer/CustomerMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<CustomerDto> _currentInstances;
        public IEnumerable<CustomerDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<CustomerDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetrieveData();
            }

            InitInstanceList();
        }

        private void InitInstanceList()
        {
            ucIList.NeedListInstances += new NeedListInstancesEventHandler(ucIList_NeedListInstances);
            ucIList.InstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIList_InstanceRowDeleting);
            ucIList.InstanceRowSaving += new InstanceRowSavingEventHandler(ucIList_InstanceRowSaving);

            PageName = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.ListLabel = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.AllowFilteringByColumn = ucIList.CurrentSubject.AllowListFiltering;

            bool allowModify = CurrentUserContext.IsEmployee || CurrentUserContext.IsAdmin;
            ucIList.AllowAdd = ucIList.CurrentSubject.AllowListAdd && allowModify;
            ucIList.AllowEdit = ucIList.CurrentSubject.AllowListEdit && allowModify;
            ucIList.AllowDelete = ucIList.CurrentSubject.AllowListDelete && allowModify;
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CustomerFacade facade = new CustomerFacade(uow);
                RetrieveInstances(facade);
            }
        }

        protected void ucIList_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        protected void ucIList_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CustomerFacade facade = new CustomerFacade(uow);
                IFacadeUpdateResult<CustomerData> result = facade.DeleteCustomer(e.Instance.Id);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    RetrieveInstances(facade);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected void ucIList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CustomerDto project = e.Instance as CustomerDto;
                CustomerFacade facade = new CustomerFacade(uow);
                IFacadeUpdateResult<CustomerData> result = facade.SaveCustomer(project);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    RetrieveInstances(facade);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsCustomer || base.CheckPagePermission();
        }

        private void RetrieveInstances(CustomerFacade facade)
        {
            if (CurrentUserContext.IsAdmin)
            {
                CurrentInstances = facade.RetrieveAllCustomer(new CustomerConverter());
            }
            else if (CurrentUserContext.IsEmployee)
            {
                CurrentInstances = facade.RetrieveAllCustomer(new CustomerConverter());
            }
            else if (CurrentUserContext.IsCustomer)
            {
                List<CustomerDto> instances = new List<CustomerDto>();
                CustomerDto instance = facade.RetrieveOrNewCustomer(CurrentUserContext.User.MatchId, new CustomerConverter());
                instances.Add(instance);
                CurrentInstances = instances;
            }
        }
    }
}
