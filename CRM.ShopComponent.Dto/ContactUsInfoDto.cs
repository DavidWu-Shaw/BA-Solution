using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.ShopComponent.Dto
{
    public class ContactUsInfoDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public DateTime IssuedTime { get; set; }
        public byte[] Attachment { get; set; }
        public object UserId { get; set; }
    }
}
