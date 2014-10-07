using System;
using Framework.Data;

namespace Setup.Data
{
    public class AdministratorData : DataObject
    {
        public AdministratorData()
        {
        }

        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual DateTime? LastConnectDate { get; set; }
        public virtual string FullName { get; set; }
    }
}
