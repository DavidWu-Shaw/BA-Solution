using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using Telerik.Web.UI;
using Web.Helpers.EventHandlers;
using BA.Web.Converters;

namespace BA.Web.Shop.UserControls
{
    public partial class SupProductExplorer : BaseControl
    {
        public const string ControlUrl = @"/Shop/UserControls/SupProductExplorer.ascx";

        public event CommandEventHandler ItemActionCommand;
        public event ButtonInitilizedEventHandler ItemActionButtonInitilized;

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

        private string _itemActionText = null;
        public string ItemActionText
        {
            get
            {
                if (string.IsNullOrEmpty(_itemActionText) && IsInViewState(InstanceStateKey))
                {
                    _itemActionText = GetFromViewState(InstanceStateKey) as string;
                }

                return _itemActionText;
            }
            set
            {
                _itemActionText = value;
                SaveInViewState(value, InstanceStateKey);
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
            lvProd.DataSource = Instances;
            lvProd.DataBind();
        }

        protected void ucCategoryTree_SelectedCategoryChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            LoadProducts(e.InstanceId);
        }

        protected void btnPreset_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts(object categoryId)
        {
            lvProd.DataSource = Instances.Where(o => object.Equals(o.CategoryId, categoryId));
            lvProd.DataBind();
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            if (ItemActionCommand != null)
            {
                ItemActionCommand(sender, e);
            }
        }

        protected void lvProd_ItemCreated(object sender, RadListViewItemEventArgs e)
        {
            Button actButton = e.Item.FindControl("btnAction") as Button;
            if (actButton != null)
            {
                if (!string.IsNullOrEmpty(ItemActionText))
                {
                    actButton.Text = ItemActionText;
                    // There is a bug here. 
                    if (ItemActionButtonInitilized != null)
                    {
                        ItemActionButtonInitilized(sender, new ButtonInitilizedEventArgs(actButton));
                    }
                }
                else
                {
                    actButton.Visible = false;
                }
            }
        }

        public string GetProductUrl(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", SupplierProductPage.PageUrl, SupplierProductPage.QryInstanceId, item.ProductId);
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
                    _textOfConst = Localize("Shop.UserControls.SupProductExplorer.NumberOfRatings", "Number of ratings:");
                }
                return _textOfConst;
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnPreset.Text = Localize("Shop.UserControls.SupProductExplorer.btnPreset", "All Products");
            lblByCat.Text = Localize("Shop.UserControls.SupProductExplorer.lblByCat", "Filter by category");
        }
    }
}