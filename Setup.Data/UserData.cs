using System;
using Framework.Data;

namespace Setup.Data
{
    public class UserData : DataObject
    {
        public UserData()
        {
        }

        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual DateTime? LastConnectDate { get; set; }
        public virtual int DomainId { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual object LanguageId { get; set; }
        public virtual object MatchId { get; set; }
        public virtual string FullName { get; set; }
    }
}
