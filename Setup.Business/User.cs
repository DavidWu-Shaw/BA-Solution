using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Business;
using Setup.Data;
using Framework.Validation;
using Framework.Security;

namespace Setup.Business
{
    public class User : BusinessObject<UserData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            CreatedDate = DateTime.Today;
            ModifiedDate = CreatedDate;
            Password = "123";
            IsActive = true;
        }

        [StringLength("UserUsernameLength", "The Username must have a length less than {1}", MaxLength = 100)]
        public string Username
        {
            get
            {
                return Data.Username;
            }
            set
            {
                Data.Username = value;
            }
        }

        [RequiredField("UserEmailRequired", "The Email must be defined.")]
        [StringLength("UserEmailLength", "The Email must have a length less than {1}", MaxLength = 100)]
        public string Email
        {
            get
            {
                return Data.Email;
            }
            set
            {
                Data.Email = value;
            }
        }

        [RequiredField("UserPasswordRequired", "The password must be defined.")]
        [StringLength("UserPasswordLength", "The password must have a length less than {1}", MaxLength = 100)]
        public string Password
        {
            get
            {
                return Data.Password;
            }
            set
            {
                if (value == null)
                {
                    Data.Password = null;
                }
                else
                {
                    Data.Password = SecurityHelper.Encrypt(value);
                }
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return Data.CreatedDate;
            }
            set
            {
                Data.CreatedDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return Data.ModifiedDate;
            }
            set
            {
                Data.ModifiedDate = value;
            }
        }

        public DateTime? LastConnectDate
        {
            get
            {
                return Data.LastConnectDate;
            }
            set
            {
                Data.LastConnectDate = value;
            }
        }

        public int DomainId
        {
            get { return Data.DomainId; }
            set { Data.DomainId = value; }
        }

        public bool IsActive
        {
            get { return Data.IsActive; }
            set { Data.IsActive = value; }
        }

        public object LanguageId
        {
            get { return Data.LanguageId; }
            set { Data.LanguageId = value; }
        }

        public object MatchId
        {
            get { return Data.MatchId; }
            set { Data.MatchId = value; }
        }

        [StringLength("UserFullNameLength", "The FullName must have a length less than {1}", MaxLength = 100)]
        public string FullName
        {
            get
            {
                return Data.FullName;
            }
            set
            {
                Data.FullName = value;
            }
        }
    }
}
