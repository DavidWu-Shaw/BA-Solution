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

namespace BA.Web.WebPages.Order
{
    public partial class OrderManager : SetupBasePage
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
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.CurrentInstance = CurrentInstance;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.OrderItem:
                    e.Instances = CurrentInstance.OrderItems;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.OrderItem:
                    e.Instance = new OrderItemDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.OrderItem:
                        OrderFacade facade = new OrderFacade(uow);
                        OrderItemDto orderItemDto = e.Instance as OrderItemDto;
                        // Save data
                        IFacadeUpdateResult<OrderData> result = facade.SaveOrderItem(CurrentInstance.Id, orderItemDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh 
                            OrderDto savedParentInstance = result.ToDto(new OrderConverter());
                            CurrentInstance.OrderItems = savedParentInstance.OrderItems;
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

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.OrderItem:
                        OrderFacade facade = new OrderFacade(uow);
                        IFacadeUpdateResult<OrderData> result = facade.DeleteOrderItem(CurrentInstance.Id, e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh whole list
                            OrderDto savedParentInstance = result.ToDto(new OrderConverter());
                            CurrentInstance.OrderItems = savedParentInstance.OrderItems;
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
    }
}