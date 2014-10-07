using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BA.Web.Common;
using CRM.ShopComponent.Dto;
using BA.Web.Common.Helper;
using Framework.Component;
using CRM.Data;
using Framework.UoW;
using BA.UnityRegistry;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;

namespace BA.Web.Shop.UserControls
{
    public partial class ProductDetail : BaseControl
    {
        public object InstanceId { get; set; }
        public bool HideSupplier { get; set; }

        private ProductInfoDto _currentInstance;
        public ProductInfoDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as ProductInfoDto;
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
            if (!IsPostBack)
            {
                RetrieveData(InstanceId);
                LoadProductDetail();
            }

            InitControls();
        }

        private void InitControls()
        {
            if (WebContext.Current.ApplicationOption.IsQuoteSupported)
            {
                hlQuote.Visible = true;
                string url = string.Format("{0}?{1}={2}", QuoteCreationWizard.PageUrl, QuoteCreationWizard.QryProductId, CurrentInstance.ProductId);
                hlQuote.NavigateUrl = GetUrl(url);
            }
            else
            {
                hlQuote.Visible = false;
            }
            btnAdd.Visible = WebContext.Current.ApplicationOption.IsShoppingSupported;
            ucReviewList.AllowAdd = !CurrentUserContext.IsAnonymous;
            ucReviewList.ReviewSaving += new ObjectSavingEventHandler(ucReviewList_ReviewSaving);
        }

        protected void ucReviewList_ReviewSaving(object sender, ObjectSavingEventArgs e)
        {
            ReviewInfoDto review = e.Instance as ReviewInfoDto;
            review.ObjectId = CurrentInstance.ProductId;
            review.IssuedById = CurrentUserContext.User.UserId;

            IFacadeUpdateResult<ReviewData> result = SaveReview(review);
            if (result.IsSuccessful)
            {
                // Refresh data
                RetrieveData(CurrentInstance.ProductId);
                LoadProductDetail();
            }
        }

        private IFacadeUpdateResult<ReviewData> SaveReview(ReviewInfoDto review)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ReviewFacade facade = new ReviewFacade(uow);
                IFacadeUpdateResult<ReviewData> result = facade.SaveProductReview(review);
                return result;
            }
        }

        private void RetrieveData(object instanceId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                CurrentInstance = facade.RetrieveProductInfo(instanceId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                // Get product reviews
                ReviewFacade reviewFacade = new ReviewFacade(uow);
                CurrentInstance.Reviews = reviewFacade.RetrieveReviewsByProduct(instanceId, new ReviewInfoConverter());
            }
        }

        private void LoadProductDetail()
        {
            ucProfile.Instance = CurrentInstance;
            ucProfile.HideSupplier = HideSupplier;
            ucProfile.LoadProfile();

            ucReviewList.Instances = CurrentInstance.Reviews;
            ucReviewList.ListDataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                UserCartFacade facade = new UserCartFacade(uow);
                facade.AddToCart(CurrentUserContext.ShoppingCart, CurrentInstance.ProductId, new ProductToCartItemConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnAdd.Text = Localize("Shop.UserControls.ProductDetail.btnAdd", "Add To Cart");
            hlQuote.Text = Localize("Shop.UserControls.MonoProductExplorer.hlQuote", "Quote Request");
        }
    }
}