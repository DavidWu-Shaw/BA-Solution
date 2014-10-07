using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;
using Telerik.Web.UI;

namespace BA.Web.WebPages.Catalog
{
    public partial class CatalogManager : SetupBasePage
    {
        public const string QryCatalogId = "CatalogId";

        private int? InstanceId { get; set; }
        private bool IsShowNodeId { get; set; }

        private CatalogDto _currentCatalog;
        private CatalogDto CurrentCatalog
        {
            get
            {
                if (_currentCatalog == null && IsPostBack && IsInSession(InstanceStateKey))
                {
                    _currentCatalog = GetFromSession(InstanceStateKey) as CatalogDto;
                }

                return _currentCatalog;
            }
            set
            {
                _currentCatalog = value;
                SaveInSession(_currentCatalog, InstanceStateKey);
            }
        }

        private enum ContextMenuKey
        {
            ShowId = 1,
            HideId = 2,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Initilize();

            if (!IsPostBack)
            {
                RetrieveData();
                LoadData();
            }
        }

        private void Initilize()
        {
            InstanceId = Request.QueryString[QryCatalogId].TryToParse<int>();

            if (!IsPostBack)
            {
                // Init context menu
                RadMenuItem item1 = new RadMenuItem();
                ConMenu.Items.Add(item1);
                item1.Value = ContextMenuKey.ShowId.ToString();
                item1.Text = "Show Node Id";
                RadMenuItem item2 = new RadMenuItem();
                ConMenu.Items.Add(item2);
                item2.Value = ContextMenuKey.HideId.ToString();
                item2.Text = "Hide Node Id";
            }
        }

        protected override bool CheckPagePermission()
        {
            AllowAnonymous = true;
            return true;
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CatalogFacade facade = new CatalogFacade(uow);
                CurrentCatalog = facade.RetrieveOrNewCatalog(InstanceId, new CatalogConverter());
                // Retrieve ScheduleEvents of Customer
                CategoryFacade categoryfacade = new CategoryFacade(uow);
                CurrentCatalog.CategoryList = categoryfacade.RetrieveCategoryTree(InstanceId, new CategoryConverter());
            }
        }

        private void LoadData()
        {
            lblCatalog.Text = "Catalog: ";
            lblName.Text = CurrentCatalog.Name;

            LoadTree();
        }

        private void LoadTree()
        {
            tvCat.DataSource = CurrentCatalog.CategoryList;
            tvCat.DataBind();
        }

        protected void tvCat_NodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            if (IsShowNodeId)
            {
                CategoryDto dataItem = e.Node.DataItem as CategoryDto;
                e.Node.Text = string.Format("{0} ({1})", dataItem.Name, dataItem.Id);
            }
        }

        protected void tvCat_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;

            if (e.MenuItem.Value == ContextMenuKey.ShowId.ToString())
            {
                IsShowNodeId = true;
                LoadTree();
            }
            else if (e.MenuItem.Value == ContextMenuKey.HideId.ToString())
            {
                IsShowNodeId = false;
                LoadTree();
            }
        }
    }
}