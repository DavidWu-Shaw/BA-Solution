using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.Criterias
{
    public class ProductCriteria
    {
        public object SupplierId { get; set; }
        public object CategoryId { get; set; }
        public string ProductName { get; set; }
        public object CatalogId { get; set; }
    }
}
