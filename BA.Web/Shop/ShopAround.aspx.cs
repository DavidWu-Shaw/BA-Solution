using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using Web.Helpers.EventHandlers;

namespace BA.Web.Shop
{
    public partial class ShopAround : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/ShopAround.aspx";

        private const string InstancesStateKey = "SuppliersSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<SupplierInfoDto> _currentInstances;
        public IEnumerable<SupplierInfoDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<SupplierInfoDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        private int? CurrentSupplerId
        {
            get
            {
                if (!string.IsNullOrEmpty(lbSup.SelectedValue))
                {
                    return Convert.ToInt32(lbSup.SelectedValue);
                }

                return null;
            }
        }

        private SupplierInfoDto _currentSupplier;
        private SupplierInfoDto CurrentSupplier
        {
            get
            {
                if (_currentSupplier == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentSupplier = GetFromSession(InstanceStateKey) as SupplierInfoDto;
                    }
                }

                return _currentSupplier;
            }
            set
            {
                _currentSupplier = value;
                SaveInSession(_currentSupplier, InstanceStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.ShopAround.PageName", "Shop around");
            InitControls();

            if (!IsPostBack)
            {
                RetrieveAllSuppliers();
                LoadSupplierList();
            }
        }

        private void InitControls()
        {
            lbSup.AutoPostBack = true;
            lbSup.SelectedIndexChanged += new EventHandler(lbSup_SelectedIndexChanged);
            lbSup.DataValueField = SupplierInfoDto.FLD_SupplierId;
            lbSup.DataTextField = SupplierInfoDto.FLD_Name;

            ucReviewList.AllowAdd = !CurrentUserContext.IsAnonymous;
            ucReviewList.ReviewSaving += new ObjectSavingEventHandler(ucReviewList_ReviewSaving);
        }

        private void RetrieveAllSuppliers()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                CurrentInstances = facade.RetrieveAllSupplier(new SupplierInfoConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void RetrieveSuppliersByCriteria(SupplierCriteria criteria)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                CurrentInstances = facade.RetrieveSuppliersBySearch(criteria, new SupplierInfoConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void LoadSupplierList()
        {
            lbSup.DataSource = CurrentInstances;
            lbSup.DataBind();

            if (lbSup.Items.Count > 0)
            {
                lbSup.SelectedIndex = 0;
            }

            LoadSelectedSupplier();
        }

        protected void lbSup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedSupplier();
        }

        private void LoadSelectedSupplier()
        {
            RetrieveSelectedSupplier();
            LoadSupplierInfo();
            ClearCartIfNeeded();
        }

        private void RetrieveSelectedSupplier()
        {
            if (CurrentSupplerId.HasValue)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    SupplierFacade facade = new SupplierFacade(uow);
                    CurrentSupplier = facade.RetrieveSupplierInfo(CurrentSupplerId, new SupplierInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                    // Get Supplier reviews
                    ReviewFacade reviewFacade = new ReviewFacade(uow);
                    CurrentSupplier.Reviews = reviewFacade.RetrieveReviewsBySupplier(CurrentSupplerId, new ReviewInfoConverter());
                    // Get Supplier products
                    ProductFacade productFacade = new ProductFacade(uow);
                    CurrentSupplier.Products = productFacade.RetrieveProductsBySupplier(CurrentSupplerId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                }
            }
        }

        private void ClearCartIfNeeded()
        {
            if (CurrentUserContext.ShoppingCart.CartItems.Count > 0)
            {
                int cartSupplierId = Convert.ToInt32(CurrentUserContext.ShoppingCart.CartItems[0].SupplierId);
                if (CurrentSupplerId.HasValue && cartSupplierId != CurrentSupplerId.Value)
                {
                    CurrentUserContext.ShoppingCart.Clear();
                }
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

        protected void ucReviewList_ReviewSaving(object sender, ObjectSavingEventArgs e)
        {
            ReviewInfoDto review = e.Instance as ReviewInfoDto;
            review.ObjectId = CurrentSupplier.SupplierId;
            review.IssuedById = CurrentUserContext.User.UserId;

            IFacadeUpdateResult<ReviewData> result = SaveReview(review);
            if (result.IsSuccessful)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    // Get product reviews
                    ReviewFacade reviewFacade = new ReviewFacade(uow);
                    ucReviewList.Instances = reviewFacade.RetrieveReviewsBySupplier(CurrentSupplier.SupplierId, new ReviewInfoConverter());
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

        private void LoadSupplierInfo()
        {
            ucProfile.Instance = CurrentSupplier;
            ucProfile.ProfileDataBind();

            LoadProducts();
            LoadReviews();
        }

        private void LoadReviews()
        {
            ucReviewList.Instances = CurrentSupplier.Reviews;
            ucReviewList.ListDataBind();
        }

        private void LoadProducts()
        {
            ucProductList.Instances = CurrentSupplier.Products;
            ucProductList.ListDataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RetrieveSuppliersByCriteria(ucSupplierCriteria.CurrentCriteria);
            LoadSupplierList();
        }
    }
}