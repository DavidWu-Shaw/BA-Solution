using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Shop
{
    public partial class OrderTracking : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/OrderTracking.aspx";
        public const string QryOrderNo = "OrderNo";

        private string OrderNumber { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.OrderTracking.PageName", "Order Tracking");

            if (!IsPostBack)
            {
                OrderNumber = Request.QueryString[QryOrderNo];
                txtNo.Text = OrderNumber;
                LoadOrder();
            }
        }

        private void LoadOrder()
        {
            OrderInfoDto order = null;
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    OrderFacade facade = new OrderFacade(uow);
                    order = facade.RetrieveOrderInfo(OrderNumber, new OrderInfoConverter());
                }

                if (order != null)
                {
                    lblMsg.Visible = false;
                    ucOrder.Visible = true;
                    ucOrder.LoadData(order);
                }
                else
                {
                    lblMsg.Visible = true;
                    ucOrder.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            OrderNumber = txtNo.Text.Trim();
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                string url = string.Format("{0}?{1}={2}", OrderTracking.PageUrl, OrderTracking.QryOrderNo, OrderNumber);
                Response.Redirect(GetUrl(url), true);
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnSearch.Text = Localize("Shop.OrderTracking.btnSearch", "Search");
            txtNo.EmptyMessage = Localize("Shop.OrderTracking.EmptyMessage", "Enter order number");
            lblMsg.Text = Localize("Shop.OrderTracking.lblMsg", "Order not found. Please verify the order number.");
        }
    }
}