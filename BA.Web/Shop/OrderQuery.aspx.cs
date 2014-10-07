using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Shop
{
    public partial class OrderQuery : AppBasePage
    {
        public const string PageUrl = @"/Shop/OrderQuery.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.OrderQuery.PageName", "Order Query");
            IsNarrowPage = true;

            InitControls();
        }

        private void InitControls()
        {
            ucSearch.SelectedInstanceChanged += new SelectedInstanceChangedEventHandler(ucSearch_SelectedInstanceChanged);
            ucOrder.Visible = false;
        }

        protected override bool CheckPagePermission()
        {
            return !CurrentUserContext.IsAnonymous;
        }

        protected void ucSearch_SelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            LoadSelectedOrder(e.InstanceId);
        }

        private void LoadSelectedOrder(object orderId)
        {
            OrderInfoDto order = null;

            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                order = facade.RetrieveOrderInfo(orderId, new OrderInfoConverter());
            }

            if (order != null)
            {
                ucOrder.Visible = true;
                ucOrder.LoadData(order);
            }
            else
            {
                ucOrder.Visible = false;
            }
        }
    }
}