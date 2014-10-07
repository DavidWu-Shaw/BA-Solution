using System;
using System.Collections.Generic;
using BA.Core;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using CRM.Core;
using CRM.Data.Criterias;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using SubjectEngine.Component;
using Telerik.Web.UI;
using Framework.Core;

namespace BA.Web.Shop.UserControls
{
    public partial class OrderSearch : BaseControl
    {
        private const string InstancesStateKey = "InstancesStateKey";

        public event SelectedInstanceChangedEventHandler SelectedInstanceChanged;

        private IEnumerable<BindingListItem> OrderStatusDataSource { get; set; }
        private IEnumerable<BindingListItem> SupplierDataSource { get; set; }

        private IEnumerable<OrderBriefInfoDto> _orders;
        private IEnumerable<OrderBriefInfoDto> Orders
        {
            get
            {
                if (_orders == null)
                {
                    if (IsInViewState(InstancesStateKey))
                    {
                        _orders = GetFromViewState(InstancesStateKey) as IEnumerable<OrderBriefInfoDto>;
                    }
                }

                return _orders;
            }
            set
            {
                _orders = value;
                SaveInViewState(value, InstancesStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControls();

            if (!IsPostBack)
            {
                RetrieveDataSource();
                LoadDDL(ddlRest, SupplierDataSource);
                LoadDDL(ddlStatus, OrderStatusDataSource);
                SetCriteria();
            }
        }

        private void InitControls()
        {
            ucOrderList.SelectedInstanceChanged += new SelectedInstanceChangedEventHandler(ucOrderList_SelectedInstanceChanged);
        }

        private void RetrieveDataSource()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade entityFacade = new DEntityFacade(uow);
                OrderStatusDataSource = entityFacade.GetEntityItemList((int)EntityTypes.OrderStatus);

                CRM.Component.SupplierFacade supplierFacade = new CRM.Component.SupplierFacade(uow);
                SupplierDataSource = supplierFacade.GetBindingList();
            }
        }

        private void LoadDDL(RadComboBox ddl, IEnumerable<BindingListItem> dataSource)
        {
            ddl.Items.Add(new RadComboBoxItem(BindingListItem.EmptyText, BindingListItem.EmptyValue));
            ddl.AppendDataBoundItems = true;
            ddl.DataValueField = BindingListItem.ValueProperty;
            ddl.DataTextField = BindingListItem.TextProperty;
            ddl.DataSource = dataSource;
            ddl.DataBind();
        }

        private void SetCriteria()
        {
            if (CurrentUserContext.IsSupplier)
            {
                txtRest.Enabled = false;
                ddlRest.Enabled = false;
                ddlRest.SelectedValue = CurrentUserContext.User.MatchId.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            OrderCriteria criteria = ComposeCriteria();
            PerformSearch(criteria);
            LoadSearchResult();
        }

        protected void ucOrderList_SelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
        {
            if (SelectedInstanceChanged != null)
            {
                SelectedInstanceChanged(this, e);
            }
        }

        private OrderCriteria ComposeCriteria()
        {
            OrderCriteria criteria = new OrderCriteria();
            if (ddlRest.SelectedItem != null && ddlRest.SelectedValue != string.Empty)
            {
                criteria.SupplierId = Convert.ToInt32(ddlRest.SelectedValue);
            }
            if (ddlStatus.SelectedItem != null && ddlStatus.SelectedValue != string.Empty)
            {
                criteria.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            criteria.OrderNumber = txtOrderNo.Text.Trim();

            return criteria;
        }

        private void PerformSearch(OrderCriteria criteria)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                Orders = facade.SearchOrders(criteria, new OrderBriefInfoConverter());
            }
        }

        private void LoadSearchResult()
        {
            ucOrderList.Orders = Orders;
            ucOrderList.LoadOrders();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblTitle.Text = Localize("Shop.UserControls.OrderSearch.lblTitle", "Search Criteria");
            lblSupplierName.Text = Localize("Shop.UserControls.OrderSearch.lblSupplierName", "Supplier Name:");
            lblSupplier.Text = Localize("Shop.UserControls.OrderSearch.lblSupplierName", "Supplier:");
            lblStatus.Text = Localize("Shop.UserControls.OrderSearch.lblStatus", "Order Status:");
            lblOrderNo.Text = Localize("Shop.UserControls.OrderSearch.lblOrderNo", "Order Number:");
            btnSearch.Text = Localize("Shop.UserControls.OrderSearch.btnSearch", "Search");
            lblList.Text = Localize("Shop.UserControls.OrderSearch.lblList", "Orders");
        }
    }
}