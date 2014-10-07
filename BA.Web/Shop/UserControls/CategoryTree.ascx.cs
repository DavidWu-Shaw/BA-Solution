using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.Component.Dto;

namespace BA.Web.Shop.UserControls
{
    public partial class CategoryTree : BaseControl
    {
        public event SelectedInstanceChangedEventHandler SelectedCategoryChanged;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildTreeView();
            }
        }

        private void BuildTreeView()
        {
            CategoryDto rootNode = CurrentInstances.FirstOrDefault(o => o.ParentId == null);
            if (rootNode != null)
            {
                // add first level nodes
                foreach (CategoryDto item in CurrentInstances.Where(o => object.Equals(o.ParentId, rootNode.Id)))
                {
                    // add node
                    TreeNode node = new TreeNode(item.Name, item.Id.ToString());
                    tvCat.Nodes.Add(node);
                    // explorer next level
                    BuildSubTree(node, 1);
                }
            }
        }

        private void BuildSubTree(TreeNode node, int level)
        {
            int nodeId = Convert.ToInt32(node.Value);
            IEnumerable<CategoryDto> childCats = CurrentInstances.Where(o => object.Equals(o.ParentId, nodeId));
            if (childCats.Count<CategoryDto>() > 0)
            {
                node.SelectAction = TreeNodeSelectAction.None;
                foreach (CategoryDto item in childCats)
                {
                    // add node
                    TreeNode childNode = new TreeNode(item.Name, item.Id.ToString());
                    node.ChildNodes.Add(childNode);
                    // explorer next level
                    BuildSubTree(childNode, level + 1);
                }
            }
        }

        protected void tvCat_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (SelectedCategoryChanged != null)
            {
                int nodeId = Convert.ToInt32(tvCat.SelectedNode.Value);
                SelectedInstanceChangedEventArgs arg = new SelectedInstanceChangedEventArgs(nodeId);
                SelectedCategoryChanged(this, arg);
            }
        }
    }
}