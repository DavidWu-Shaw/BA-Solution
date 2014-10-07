using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.Criterias
{
    public class SupplierCriteria
    {
        public string SupplierName { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public object CategoryId { get; set; }
    }
}
