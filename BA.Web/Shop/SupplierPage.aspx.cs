using System;
using System.Web;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using Web.Helpers.EventHandlers;

namespace BA.Web.Shop
{
    public partial class SupplierPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/SupplierPage.aspx";
        public const string QryInstanceId = "SupplierId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryInstanceId].TryToParse<int>();
            }
        }

        private SupplierInfoDto _currentInstance;
        private SupplierInfoDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as SupplierInfoDto;
                    }
                }

                return _currentInstance;
            }
            set
            {
                _currentInstance = value;
                SaveInSession(_currentInstance, InstanceStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.SupplierPage.PageName", "Supplier home");
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            InitControls();

            if (!IsPostBack)
            {
                ClearCartIfNeed();
                RetrieveData();
                LoadSupplier();
                LoadCartBrief();
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            SiteMap.SiteMapResolve -= SiteMap_SiteMapResolve;
            base.OnPreRenderComplete(e);
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            if (currentNode != null)
            {
                currentNode.Title = CurrentInstance.Name;
            }
            return currentNode;
        }

        private void ClearCartIfNeed()
        {
            if (CurrentUserContext.ShoppingCart.CartItems.Count > 0)
            {
                int cartSupplierId = Convert.ToInt32(CurrentUserContext.ShoppingCart.CartItems[0].SupplierId);
                if (cartSupplierId != InstanceId.Value)
                {
                    CurrentUserContext.ShoppingCart.Clear();
                }
            }
        }

        private void InitControls()
        {
            if (WebContext.Current.ApplicationOption.IsShoppingSupported)
            {
                ucCartBrief.Visible = true;
                ucProductExplorer.ItemActionText = Localize("Shop.SupplierPage.AddCart", "Add to Cart");
                ucProductExplorer.ItemActionButtonInitilized += new ButtonInitilizedEventHandler(ucProductList_ItemActionButtonInitilized);
                ucProductExplorer.ItemActionCommand += new CommandEventHandler(ucProductList_ItemActionCommand);
            }

            ucReviewList.AllowAdd = !CurrentUserContext.IsAnonymous;
            ucReviewList.ReviewSaving += new ObjectSavingEventHandler(ucReviewList_ReviewSaving);
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

            LoadCartBrief();
        }

        protected void ucReviewList_ReviewSaving(object sender, ObjectSavingEventArgs e)
        {
            ReviewInfoDto review = e.Instance as ReviewInfoDto;
            review.ObjectId = CurrentInstance.SupplierId;
            review.IssuedById = CurrentUserContext.User.UserId;

            IFacadeUpdateResult<ReviewData> result = SaveReview(review);
            if (result.IsSuccessful)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    // Get reviews
                    ReviewFacade reviewFacade = new ReviewFacade(uow);
                    ucReviewList.Instances = reviewFacade.RetrieveReviewsBySupplier(CurrentInstance.SupplierId, new ReviewInfoConverter());
                    ucReviewList.ListDataBind();
                }
            }
            else
            {
                ProcUpdateResult(result.ValidationResult, result.Exception);
            }
        }

        private IFacadeUpdateResult<ReviewData> SaveReview(ReviewInfoDto review)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ReviewFacade facade = new ReviewFacade(uow);
                IFacadeUpdateResult<ReviewData> result = facade.SaveSupplierReview(review);
                return result;
            }
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                CurrentInstance = facade.RetrieveSupplierInfo(InstanceId, new SupplierInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                // Get Supplier reviews
                ReviewFacade reviewFacade = new ReviewFacade(uow);
                CurrentInstance.Reviews = reviewFacade.RetrieveReviewsBySupplier(InstanceId, new ReviewInfoConverter());
                // Get Supplier products
                ProductFacade productFacade = new ProductFacade(uow);
                CurrentInstance.Products = productFacade.RetrieveProductsBySupplier(InstanceId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void LoadSupplier()
        {
            ucProfile.Instance = CurrentInstance;
            ucProfile.ProfileDataBind();

            LoadProducts();
            LoadReviews();
        }

        private void LoadReviews()
        {
            ucReviewList.Instances = CurrentInstance.Reviews;
            ucReviewList.ListDataBind();
        }

        private void LoadProducts()
        {
            ucProductExplorer.CatalogId = CurrentInstance.ProductCatalogId;
            ucProductExplorer.Instances = CurrentInstance.Products;
        }

        private void LoadCartBrief()
        {
            ucCartBrief.Cart = CurrentUserContext.ShoppingCart;
            ucCartBrief.LoadData();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            rTab.Tabs.FindTabByValue("tabProduct").Text = Localize("Shop.SupplierPage.ProductTabTitle", "Products");
            rTab.Tabs.FindTabByValue("tabReview").Text = Localize("Shop.SupplierPage.ReviewTabTitle", "Reviews");
        }
    }
}