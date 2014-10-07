using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.ShopComponent.Dto;
using CRM.Component.Dto;
using SFA.Web.Helpers;

namespace SFA.Web.Models
{
    public class ProductExplorerViewModel
    {
        public object CurrentCategoryId { get; set; }
        public CategoryNode CategoryTree { get; set; }
    }
}