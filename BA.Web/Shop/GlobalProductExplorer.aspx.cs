using System;
using System.Web.UI.WebControls;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using Framework.UoW;
using Web.Helpers.EventHandlers;

namespace BA.Web.Shop
{
    public partial class GlobalProductExplorer : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/GlobalProductExplorer.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.GlobalProductExplorer.PageName", "Global Products");

            InitControls();

            if (!IsPostBack)
            {
                ucProductExplorer.CatalogId = WebContext.Current.ApplicationOption.GlobalProductCatalogId;
                LoadProducts();
            }
        }

        private void InitControls()
        {
            if (WebContext.Current.ApplicationOption.IsShoppingSupported)
            {
                ucProductExplorer.ItemActionText = Localize("Shop.GlobalProductExplorer.AddCart", "Add to Cart");
                ucProductExplorer.ItemActionButtonInitilized += new ButtonInitilizedEventHandler(ucProductList_ItemActionButtonInitilized);
                ucProductExplorer.ItemActionCommand += new CommandEventHandler(ucProductList_ItemActionCommand);
            }
        }

        private void LoadProducts()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                ucProductExplorer.Instances = facade.RetrieveGlobalProducts(WebContext.Current.ApplicationOption.GlobalProductCatalogId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void ucProductList_ItemActionButtonInitilized(object sender, ButtonInitilizedEventArgs e)
        {
            //amProd.AjaxSettings.AddAjaxSetting(e.ButtonControl, ucCartBrief.RadListViewControl);
        }

        private void ucProductList_ItemActionCommand(object sender, CommandEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                UserCartFacade facade = new UserCartFacade(uow);
                object productId = Convert.ToInt32(e.CommandArgument);
                facade.AddToCart(CurrentUserContext.ShoppingCart, productId, new ProductToCartItemConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }
    }
}