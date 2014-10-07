using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    public enum UserDomain
    {
        Unknown = 0,
        SysAdmin = 1,
        Employee = 2,
        Agent = 3,
        Customer = 4
    }

    public class UserData : DataObject
    {        
        public UserData()
        {
        }

        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; } 
        public virtual string FullName { get; set; }
		public virtual int? DomainId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
		public virtual DateTime? UpdateDate { get; set; } 
		public virtual int? MenuSettingId { get; set; } 
		public virtual int? UserLanguage { get; set; } 
		public virtual int? Theme { get; set; } 
		public virtual bool? IsBuiltInUser { get; set; } 
		public virtual bool? IsActive { get; set; } 
		public virtual int? LanguageId { get; set; } 
		public virtual int? CalendarId { get; set; } 
    }
}
