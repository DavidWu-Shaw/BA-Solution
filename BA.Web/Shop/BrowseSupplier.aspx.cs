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

namespace BA.Web.Shop
{
    public partial class BrowseSupplier : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/BrowseSupplier.aspx";

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

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.BrowseSupplier.PageName", "Browse Suppliers");

            if (!IsPostBack)
            {
                LoadCategoryData();
                RetrievePopularSuppliers();
                LoadSuppliersInRotate();
            }
        }

        private void LoadCategoryData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CRM.Component.CategoryFacade facade = new CRM.Component.CategoryFacade(uow);
                ucCategoryTree.CurrentInstances = facade.RetrieveCategoryTree(WebContext.Current.ApplicationOption.SupplierCatalogId, new CategoryConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void RetrievePopularSuppliers()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                CurrentInstances = facade.RetrieveAllSupplier(new SupplierInfoConverter(CurrentUserContext.CurrentLanguage.Id));
            }
        }

        private void LoadSuppliersInRotate()
        {
            lblTitle.Text = Localize("Shop.BrowseSupplier.lblTitle1", "Popular Suppliers");
            rotProd.DataSource = CurrentInstances;
            rotProd.DataBind();
        }

        private void LoadSuppliers()
        {
            lvProd.DataSource = CurrentInstances;
            lvProd.DataBind();
        }

        protected void ucCategoryTree_SelectedCategoryChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            dvRotator.Visible = false;
            dvList.Visible = true;
            lblTitle.Text = Localize("Shop.BrowseSupplier.lblTitle2", "Filtered Suppliers");
            LoadSuppliers(e.InstanceId);
        }

        protected void btnPreset_Click(object sender, EventArgs e)
        {
            dvRotator.Visible = false;
            dvList.Visible = true;
            lblTitle.Text = Localize("Shop.BrowseSupplier.lblTitle3", "Popular Suppliers");
            RetrievePopularSuppliers();
            LoadSuppliers();
        }

        private void LoadSuppliers(object categoryId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                CurrentInstances = facade.RetrieveSuppliersByCategory(categoryId, new SupplierInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                lvProd.DataSource = CurrentInstances;
                lvProd.DataBind();
            }
        }

        public string GetNavigateUrl(object obj)
        {
            SupplierInfoDto item = (SupplierInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", SupplierPage.PageUrl, SupplierPage.QryInstanceId, item.SupplierId);
            return GetUrl(url);
        }

        public string GetNumberOfRatingsDisplay(object obj)
        {
            SupplierInfoDto item = (SupplierInfoDto)obj;
            return string.Format("Number of ratings: {0}", item.NumberOfRatings);
        }

        public string GetOpeningHour(object obj)
        {
            SupplierInfoDto item = (SupplierInfoDto)obj;
            string startTime = string.Empty;
            string endTime = string.Empty;
            if (item.DayStartTime.HasValue)
            {
                startTime = item.DayStartTime.Value.ToShortTimeString();
            }
            if (item.DayEndTime.HasValue)
            {
                endTime = item.DayEndTime.Value.ToShortTimeString();
            }
            return string.Format("{0} - {1}", startTime, endTime);
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnPreset.Text = Localize("Shop.BrowseSupplier.btnPreset", "Popular Suppliers");
            lblFilter.Text = Localize("Shop.BrowseSupplier.lblFilter", "Filter By");
            lblByCat.Text = Localize("Shop.BrowseSupplier.lblByCat", "By Category");
            lblByCriteria.Text = Localize("Shop.BrowseSupplier.lblByCriteria", "By Criteria");
            btnSearch.Text = Localize("Shop.BrowseSupplier.btnSearch", "Search");
        }
    }
}