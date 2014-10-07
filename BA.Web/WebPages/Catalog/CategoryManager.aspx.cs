using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;
using Telerik.Web.UI;

namespace BA.Web.WebPages.Catalog
{
    public partial class CategoryManager : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/Category/CategoryManager.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<CategoryDto> _currentInstances;
        public IEnumerable<CategoryDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<CategoryDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        private bool IsShowNodeId { get; set; }

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
                CategoryFacade facade = new CategoryFacade(uow);
                CurrentInstances = facade.RetrieveAllCategory(new CategoryConverter());
            }
        }

        private void LoadData()
        {
            tvCat.DataSource = CurrentInstances;
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
                LoadData();
            }
            else if (e.MenuItem.Value == ContextMenuKey.HideId.ToString())
            {
                IsShowNodeId = false;
                LoadData();
            }
        }
    }
}