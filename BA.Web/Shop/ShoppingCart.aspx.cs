using System;
using BA.Web.Common;
using CRM.ShopComponent;

namespace BA.Web.Shop
{
    public partial class ShoppingCart : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/ShoppingCart.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.ShoppingCart.PageName", "Shopping Cart");

            InitControls();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void InitControls()
        {
            btnCheckout.PostBackUrl = GetUrl(CheckoutWizard.PageUrl);
        }

        private void LoadData()
        {
            ucCart.Cart = CurrentUserContext.ShoppingCart;
            ucCart.LoadData();

            if (CurrentUserContext.IsCartEmpty)
            {
                btnCheckout.Enabled = false;
                btnClearCart.Enabled = false;
            }
        }

        protected void btnClearCart_Click(object sender, EventArgs e)
        {
            UserCartFacade.ClearCart(CurrentUserContext.ShoppingCart);
            LoadData();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnCheckout.Text = Localize("Shop.ShoppingCart.btnCheckout", "Checkout");
            btnClearCart.Text = Localize("Shop.ShoppingCart.btnClearCart", "Clear Cart");
        }
    }
}