using System;
using BA.Web.Common;
using CRM.ShopComponent.Dto;
using CRM.Core;

namespace BA.Web.UserControls
{
    public partial class OrderItemsView : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void LoadData(OrderInfoDto order)
        {
            lvItems.DataSource = order.OrderItems;
            lvItems.DataBind();

            lblQtyOrderedTotal.Text = order.QtyOrderedTotal.ToString();
            lblTotalAmount.Text = order.Amount.ToString();
        }

        public string GetHeaderOfItem()
        {
            return Localize("UserControls.OrderItemsView.HeaderOfItem", "Item");
        }

        public string GetHeaderOfPrice()
        {
            return Localize("UserControls.OrderItemsView.HeaderOfPrice", "Price");
        }

        public string GetHeaderOfQuantity()
        {
            return Localize("UserControls.OrderItemsView.HeaderOfQuantity", "Quantity");
        }

        public string GetHeaderOfAmount()
        {
            return Localize("UserControls.OrderItemsView.HeaderOfAmount", "Amount");
        }

        public string GetItemName(object obj)
        {
            OrderItemInfoDto item = (OrderItemInfoDto)obj;
            return WebContext.GetLocalizedData(CommonConst.ProductNameKeyFormatString, item.ProductId, CurrentUserContext.CurrentLanguage.Id, item.ProductName);
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblTitle.Text = Localize("UserControls.OrderItemsView.lblTitle", "Order Items");
            lblSubTotal.Text = Localize("UserControls.OrderItemsView.lblSubTotal", "Sub Total");
        }
    }
}