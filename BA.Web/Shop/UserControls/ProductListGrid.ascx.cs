using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BA.Web.Common;
using CRM.ShopComponent.Dto;
using Telerik.Web.UI;

namespace BA.Web.Shop.UserControls
{
    public partial class ProductListGrid : BaseControl
    {
        private const string KeyFieldName = "ProductId";
        private const string InstancesStateKey = "ProductsSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<ProductInfoDto> _currentInstances;
        public IEnumerable<ProductInfoDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<ProductInfoDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                RemoveFromSession(UniqueInstancesStateKey);
            }

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DefineColumns();
                InitData();
            }
        }

        private void DefineColumns()
        {
            gvProd.MasterTableView.DataKeyNames = new string[] { KeyFieldName };

            GridHyperLinkColumn c2 = new GridHyperLinkColumn();
            gvProd.MasterTableView.Columns.Add(c2);
            c2.HeaderText = Localize("Shop.UserControls.ProductListGrid.HeaderTextName", "Product Name"); 
            c2.DataTextField = ProductInfoDto.FLD_Name;
            c2.DataNavigateUrlFormatString = string.Format("{0}?{1}={{0}}", ServerPath + ProductPage.PageUrl, ProductPage.QryInstanceId);
            c2.DataNavigateUrlFields = new string[] { ProductInfoDto.FLD_ProductId };
            c2.ItemStyle.Width = Unit.Pixel(160);

            GridBoundColumn c3 = new GridBoundColumn();
            gvProd.MasterTableView.Columns.Add(c3);
            c3.HeaderText = Localize("Shop.UserControls.ProductListGrid.HeaderTextPrice", "Unit Price");
            c3.DataField = ProductInfoDto.FLD_UnitPrice;
            c3.ItemStyle.Width = Unit.Pixel(60);

            GridBoundColumn c7 = new GridBoundColumn();
            gvProd.MasterTableView.Columns.Add(c7);
            c7.HeaderText = Localize("Shop.UserControls.ProductListGrid.HeaderTextUom", "Unit of Measure");
            c7.DataField = ProductInfoDto.FLD_UnitOfMeasure;
            c7.ItemStyle.Width = Unit.Pixel(80);

            GridBoundColumn c5 = new GridBoundColumn();
            gvProd.MasterTableView.Columns.Add(c5);
            c5.HeaderText = Localize("Shop.UserControls.ProductListGrid.HeaderTextPackaging", "Packaging");
            c5.DataField = ProductInfoDto.FLD_Packaging;
            c5.ItemStyle.Width = Unit.Pixel(80);

            GridBoundColumn c4 = new GridBoundColumn();
            gvProd.MasterTableView.Columns.Add(c4);
            c4.HeaderText = Localize("Shop.UserControls.ProductListGrid.HeaderTextDescription", "Description");
            c4.DataField = ProductInfoDto.FLD_Description;
            c4.ItemStyle.Width = Unit.Pixel(200);

            GridHyperLinkColumn c1 = new GridHyperLinkColumn();
            gvProd.MasterTableView.Columns.Add(c1);
            c1.HeaderText = Localize("Shop.UserControls.ProductListGrid.HeaderTextSupplier", "Supplier");
            c1.DataTextField = ProductInfoDto.FLD_SupplierName;
            c1.DataNavigateUrlFormatString = string.Format("{0}?{1}={{0}}", ServerPath + SupplierPage.PageUrl, SupplierPage.QryInstanceId);
            c1.DataNavigateUrlFields = new string[] { SupplierInfoDto.FLD_SupplierId };
            c1.ItemStyle.Width = Unit.Pixel(120);
        }

        private void InitData()
        {
            CurrentInstances = new List<ProductInfoDto>();
            gvProd.DataBind();
        }

        public void ListDataBind()
        {
            gvProd.Rebind();
        }

        protected void gvProd_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            gvProd.DataSource = CurrentInstances;
        }
    }
}