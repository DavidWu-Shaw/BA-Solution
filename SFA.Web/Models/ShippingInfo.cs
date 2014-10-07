using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA.Web.Models
{
    public class ShippingInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public DateTime? TimeShipBy { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
    }
}