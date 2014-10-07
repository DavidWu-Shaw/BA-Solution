using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Shop.UserControls
{
    // In this user control, Products show as grid view
    public partial class ProductExplorer : BaseControl
    {
        public const string ControlUrl = @"/Shop/UserControls/ProductExplorer.ascx";

        private const string InstancesStateKey = "ProductsSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        public object CatalogId { get; set; }

        private IEnumerable<ProductInfoDto> _instances;
        public IEnumerable<ProductInfoDto> Instances
        {
            get
            {
                if (_instances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _instances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<ProductInfoDto>;
                }

                return _instances;
            }
            set
            {
                _instances = value;
                SaveInSession(_instances, UniqueInstancesStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetrieveCategoryData();
                LoadProducts();
            }
        }

        private void RetrieveCategoryData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CRM.Component.CategoryFacade facade = new CRM.Component.CategoryFacade(uow);
                ucCategoryTree.CurrentInstances = facade.RetrieveCategoryTree(CatalogId, new CategoryConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void LoadProducts()
        {
            lblTitle.Text = Localize("Shop.UserControls.ProductExplorer.lblTitle1", "Top Sellers");
            rotProd.DataSource = Instances;
            rotProd.DataBind();
        }

        protected void ucCategoryTree_SelectedCategoryChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            dvRotator.Visible = false;
            dvList.Visible = true;
            LoadProducts(e.InstanceId);
        }

        protected void btnPreset_Click(object sender, EventArgs e)
        {
            dvRotator.Visible = false;
            dvList.Visible = true;
            lblTitle.Text = Localize("Shop.UserControls.ProductExplorer.lblTitle2", "Top Sellers");
            ucProductList.CurrentInstances = Instances;
            ucProductList.ListDataBind();
        }

        private void LoadProducts(object categoryId)
        {
            lblTitle.Text = Localize("Shop.UserControls.ProductExplorer.lblTitle3", "Filtered Products");
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                ucProductList.CurrentInstances = facade.RetrieveProductsByCategory(categoryId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                ucProductList.ListDataBind();
            }
        }

        public string GetProductUrl(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", ProductPage.PageUrl, ProductPage.QryInstanceId, item.ProductId);
            return GetUrl(url);
        }

        public string GetSupplierUrl(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", SupplierPage.PageUrl, SupplierPage.QryInstanceId, item.SupplierId);
            return GetUrl(url);
        }

        public string GetNumberOfRatingsDisplay(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            return string.Format("{0} {1}", TextOfConst, item.NumberOfRatings);
        }

        private string _textOfConst;
        private string TextOfConst
        {
            get
            {
                if (_textOfConst == null)
                {
                    _textOfConst = Localize("Shop.UserControls.ProductExplorer.NumberOfRatings", "Number of ratings:");
                }
                return _textOfConst;
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnPreset.Text = Localize("Shop.UserControls.ProductExplorer.btnPreset", "Top Sellers");
            lblFilter.Text = Localize("Shop.UserControls.ProductExplorer.lblFilter", "Filter By");
            lblByCat.Text = Localize("Shop.UserControls.ProductExplorer.lblByCat", "By Category");
        }
    }
}