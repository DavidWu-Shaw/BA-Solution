using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BA.Web.Shop
{
    public class ShippingInfo
    {
        public string ContactPhone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public DateTime? TimeShipBy { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
    }
}