using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Component.Dto;

namespace SFA.Web.Helpers
{
    public class CategoryNode
    {
        public CategoryNode()
        {
            SubNodes = new List<CategoryNode>();
        }

        public CategoryNode(string name, object id)
        {
            Name = name;
            Id = id;
            SubNodes = new List<CategoryNode>();
        }

        public string Name { get; set; }
        public object Id { get; set; }
        public List<CategoryNode> SubNodes { get; set; }
    }

    public class CategoryTreeBuilder
    {
        public CategoryNode CategoryTree { get; set; }
        private IEnumerable<CategoryDto> NodeList { get; set; }

        public CategoryTreeBuilder(IEnumerable<CategoryDto> nodes)
        {
            NodeList = nodes;
            BuildTree();
        }

        private void BuildTree()
        {
            CategoryTree = new CategoryNode();

            CategoryDto rootNode = NodeList.FirstOrDefault(o => o.ParentId == null);
            if (rootNode != null)
            {
                CategoryTree.Name = rootNode.Name;
                // add first level nodes
                foreach (CategoryDto item in NodeList.Where(o => object.Equals(o.ParentId, rootNode.Id)))
                {
                    // add node
                    CategoryNode node = new CategoryNode(item.Name, item.Id);
                    CategoryTree.SubNodes.Add(node);
                    // explorer next level
                    BuildSubTree(node, 1);
                }
            }
        }

        private void BuildSubTree(CategoryNode node, int level)
        {
            IEnumerable<CategoryDto> childCats = NodeList.Where(o => object.Equals(o.ParentId, node.Id));
            if (childCats.Count<CategoryDto>() > 0)
            {
                foreach (CategoryDto item in childCats)
                {
                    // add node
                    CategoryNode childNode = new CategoryNode(item.Name, item.Id);
                    node.SubNodes.Add(childNode);
                    // explorer next level
                    BuildSubTree(childNode, level + 1);
                }
            }
        }

    }
}