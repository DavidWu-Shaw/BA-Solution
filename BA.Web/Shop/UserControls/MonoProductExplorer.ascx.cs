using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using Telerik.Web.UI;
using Web.Helpers.EventHandlers;

namespace BA.Web.Shop.UserControls
{
    // In this user control, the Supplier is not showed up
    public partial class MonoProductExplorer : BaseControl
    {
        public const string ControlUrl = @"/Shop/UserControls/MonoProductExplorer.ascx";

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

        protected void ucCategoryTree_SelectedCategoryChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            LoadProducts(e.InstanceId);
        }

        protected void btnPreset_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            lblTitle.Text = Localize("Shop.UserControls.MonoProductExplorer.lblTitle2", "Top Sellers");
            lvProd.DataSource = Instances;
            lvProd.DataBind();
        }

        private void LoadProducts(object categoryId)
        {
            lblTitle.Text = Localize("Shop.UserControls.MonoProductExplorer.lblTitle3", "Filtered Products");
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
            HyperLink lnk = e.Item.FindControl("hlQuote") as HyperLink;
            if (lnk != null)
            {
                lnk.Visible = WebContext.Current.ApplicationOption.IsQuoteSupported;
                lnk.Text = Localize("Shop.UserControls.MonoProductExplorer.hlQuote", "Quote Request");
            }
            Button actButton = e.Item.FindControl("btnAction") as Button;
            if (actButton != null)
            {
                if (WebContext.Current.ApplicationOption.IsShoppingSupported)
                {
                    actButton.Visible = true;
                    if (!string.IsNullOrEmpty(ItemActionText))
                    {
                        actButton.Text = ItemActionText;
                    }
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

        public string GetQuoteUrl(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", QuoteCreationWizard.PageUrl, QuoteCreationWizard.QryProductId, item.ProductId);
            return GetUrl(url);
        }

        public string GetProductUrl(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", MonoProductPage.PageUrl, MonoProductPage.QryInstanceId, item.ProductId);
            return GetUrl(url);
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            btnPreset.Text = Localize("Shop.UserControls.MonoProductExplorer.btnPreset", "Top Sellers");
            lblFilter.Text = Localize("Shop.UserControls.MonoProductExplorer.lblFilter", "Filter By");
            lblByCat.Text = Localize("Shop.UserControls.MonoProductExplorer.lblByCat", "By Category");
        }
    }
}