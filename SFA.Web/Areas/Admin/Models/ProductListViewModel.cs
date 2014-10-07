using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Component.Dto;
using SFA.Web.Common;

namespace SFA.Web.Areas.Admin.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}