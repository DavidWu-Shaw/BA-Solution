using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using Framework.UoW;

namespace BA.Web.Shop
{
    public partial class BrowseProduct : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/BrowseProduct.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.BrowseProduct.PageName", "Browse Products");

            if (!IsPostBack)
            {
                ucProductExplorer.CatalogId = WebContext.Current.ApplicationOption.GlobalProductCatalogId;
                RetrievePopularProducts();
            }
        }

        private void RetrievePopularProducts()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                ucProductExplorer.Instances = facade.RetrieveGlobalProducts(WebContext.Current.ApplicationOption.GlobalProductCatalogId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }
    }
}