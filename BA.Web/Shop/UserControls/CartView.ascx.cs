using BA.Web.Common;
using CRM.Core;
using CRM.ShopComponent.Dto;
using System;

namespace BA.Web.Shop.UserControls
{
    public partial class CartView : BaseControl
    {
        public CartDto Cart { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void LoadData()
        {
            lvItem.DataSource = Cart.CartItems;
            lvItem.DataBind();

            lblQtyTotal.Text = Cart.QtyOrderedTotal.ToString();
            lblTotalAmount.Text = Cart.TotalAmount.ToString();
        }

        public string GetHeaderOfItem()
        {
            return Localize("Shop.UserControls.CartBrief.HeaderOfItem", "Item");
        }

        public string GetHeaderOfPrice()
        {
            return Localize("Shop.UserControls.CartBrief.HeaderOfPrice", "Price");
        }

        public string GetHeaderOfQuantity()
        {
            return Localize("Shop.UserControls.CartBrief.HeaderOfQuantity", "Quantity");
        }

        public string GetHeaderOfAmount()
        {
            return Localize("Shop.UserControls.CartBrief.HeaderOfAmount", "Amount");
        }

        public string GetItemName(object obj)
        {
            CartItemDto item = (CartItemDto)obj;
            return WebContext.GetLocalizedData(CommonConst.ProductNameKeyFormatString, item.ProductId, CurrentUserContext.CurrentLanguage.Id, item.ProductName);
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblTitle.Text = Localize("Shop.UserControls.CartBrief.lblTitle", "Shopping Cart");
            lblTotal.Text = Localize("Shop.UserControls.CartBrief.lblTotal", "Sub Total");
        }
    }
}