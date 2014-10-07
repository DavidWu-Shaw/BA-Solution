using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Setup.Data;
using Setup.Core;

namespace Setup.Component
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity()
        {
            DomainId = (int)UserDomains.Anonymous;
        }

        public UserIdentity(UserData user)
        {
            UserId = user.Id;
            Email = user.Email;
            if (user.Username.HasValue())
            {
                Username = user.Username;
            }
            else
            {
                Username = user.Email;
            }
            LastConnectDate = user.LastConnectDate;
            DomainId = user.DomainId;
            MatchId = user.MatchId;
            LanguageId = user.LanguageId;
        }

        public UserIdentity(AdministratorData user)
        {
            Email = user.Email;
            if (user.Username.HasValue())
            {
                Username = user.Username;
            }
            else
            {
                Username = user.Email;
            }
            LastConnectDate = user.LastConnectDate;
            DomainId = (int)UserDomains.SysAdmin;
        }

        public object UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? LastConnectDate { get; set; }
        public int DomainId { get; set; }
        public object MatchId { get; set; }
        public object LanguageId { get; set; }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "forms"; }
        }

        public bool IsAuthenticated
        {
            get
            {
                return DomainId != (int)UserDomains.Anonymous;
            }
        }

        public string Name
        {
            get { return Username; }
        }

        #endregion
    }
}
