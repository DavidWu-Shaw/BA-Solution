using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.Criterias
{
    public class OrderCriteria
    {
        public object SupplierId { get; set; }
        public string SupplierName { get; set; }
        public object CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? StatusId { get; set; }
        public string OrderNumber { get; set; }
    }
}
