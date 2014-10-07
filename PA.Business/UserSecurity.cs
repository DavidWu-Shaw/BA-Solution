using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Framework.Business;
using PA.Data;

namespace PA.Business
{
    [Serializable]
    public class UserSecurity
    {
        private UserData _userData;

        public UserSecurity()
        {
        }

        public UserSecurity(UserData userData)
        {
            _userData = userData;
        }

        public UserData UserData
        {
            get
            {
                return _userData;
            }
        }
    }

    public enum AuthenticationFailureType
    {
        InvalidUserName = 1, 
        InvalidPassword = 2
    }

    public class UserAuthenticationResult
    {
        public bool IsSuccessful { get; set; }
        public AuthenticationFailureType FailureType { get; set; }
        public UserData AuthenticatedUserData { get; set; }

        public UserAuthenticationResult()
        {
        }
    }
}

