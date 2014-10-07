using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using CRM.Core;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.WebPages.Order
{
    public partial class OrderProcess : AppBasePage
    {
        public const string PageUrl = @"/WebPages/Order/OrderProcess.aspx";

        private IEnumerable<OrderBriefInfoDto> NewOrders { get; set; }
        private IEnumerable<OrderBriefInfoDto> InProcessOrders { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Order Process";

            InitControls();
            if (!IsPostBack)
            {
                LoadListsData();
            }
        }

        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsSupplier || CurrentUserContext.IsAdmin;
        }

        private void InitControls()
        {
            oNew.SelectedInstanceChanged += new SelectedInstanceChangedEventHandler(ucNewOrders_SelectedInstanceChanged);
            oInProc.SelectedInstanceChanged += new SelectedInstanceChangedEventHandler(ucOrders_SelectedInstanceChanged);
            ucOrder.OrderChanged += new InstanceChangedEventHandler(ucOrder_OrderChanged);
        }

        private void LoadListsData()
        {
            NewOrders = RetrieveNewOrders();
            InProcessOrders = RetrieveInProcessOrders();
            LoadNewOrders();
            LoadInOrders();
        }

        protected void ucOrder_OrderChanged(object sender, InstanceChangedEventArgs e)
        {
            LoadListsData();
            OrderInfoDto order = e.Instance as OrderInfoDto;
            LoadSelectedOrder(order.OrderId);
        }

        protected void ucOrders_SelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            LoadSelectedOrder(e.InstanceId);
            oNew.ClearListSelection();
        }

        protected void ucNewOrders_SelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            LoadSelectedOrder(e.InstanceId);
            oInProc.ClearListSelection();
        }

        private void LoadNewOrders()
        {
            oNew.Orders = NewOrders;
            oNew.LoadOrders();
        }

        private void LoadInOrders()
        {
            oInProc.Orders = InProcessOrders;
            oInProc.LoadOrders();
        }

        private void LoadSelectedOrder(object orderId)
        {
            OrderInfoDto order = null;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                order = facade.RetrieveOrderInfo(orderId, new OrderInfoConverter());
                order.OrderCommands = facade.GetOrderCommands(order.StatusId);
            }

            LoadOrderInfo(order);
        }

        private void LoadOrderInfo(OrderInfoDto order)
        {
            if (order != null)
            {
                ucOrder.Visible = true;
                ucOrder.Order = order;
                ucOrder.LoadData();
            }
        }

        private IEnumerable<OrderBriefInfoDto> RetrieveNewOrders()
        {
            IEnumerable<OrderBriefInfoDto> instances = new List<OrderBriefInfoDto>();
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                if (CurrentUserContext.IsSuperAdmin)
                {
                    instances = facade.RetrieveOrdersByStatus(OrderStatuses.Open, new OrderBriefInfoConverter());
                }
                else if (CurrentUserContext.IsSupplier)
                {
                    instances = facade.RetrieveOrdersBySupplierAndStatus(CurrentUserContext.User.MatchId, OrderStatuses.Open, new OrderBriefInfoConverter());
                }
            }
            return instances;
        }

        private IEnumerable<OrderBriefInfoDto> RetrieveInProcessOrders()
        {
            IEnumerable<OrderBriefInfoDto> instances = new List<OrderBriefInfoDto>();
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                if (CurrentUserContext.IsSupplier)
                {
                    instances = facade.RetrieveOrdersInProcessBySupplier(CurrentUserContext.User.MatchId, new OrderBriefInfoConverter());
                }
                else if (CurrentUserContext.IsSuperAdmin)
                {
                    instances = facade.RetrieveOrdersInProcess(new OrderBriefInfoConverter());
                }
            }

            return instances;
        }
    }
}