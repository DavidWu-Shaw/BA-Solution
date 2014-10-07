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

namespace BA.Web.WebPages.Order
{
    public partial class OrderEdit : SetupBasePage
    {
        public const string QryOrderId = "OrderId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryOrderId].TryToParse<int>();
            }
        }

        private OrderDto _currentInstance;
        private OrderDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            OrderFacade facade = new OrderFacade(uow);
                            _currentInstance = facade.RetrieveOrNewOrder(InstanceId, new OrderConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as OrderDto;
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
            if ((CurrentUserContext.IsSupplier && object.Equals(CurrentInstance.SupplierId, CurrentUserContext.User.MatchId))
                || (CurrentUserContext.IsCustomer && object.Equals(CurrentInstance.CustomerId, CurrentUserContext.User.MatchId)))
            {
                return true;
            }
            return base.CheckPagePermission();
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
                OrderFacade facade = new OrderFacade(uow);
                OrderDto instance = facade.RetrieveOrNewOrder(InstanceId, new OrderConverter());
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
            OrderDto instance = e.Instance as OrderDto;
            if (instance != null)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    OrderFacade facade = new OrderFacade(uow);
                    IFacadeUpdateResult<OrderData> result = facade.SaveOrder(instance);
                    e.IsSuccessful = result.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        // Refresh Instance
                        CurrentInstance = result.ToDto<OrderDto>(new OrderConverter());
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