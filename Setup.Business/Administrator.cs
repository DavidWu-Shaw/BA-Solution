using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Business;
using Setup.Data;
using Framework.Validation;

namespace Setup.Business
{
    public class Administrator : BusinessObject<AdministratorData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            CreatedDate = DateTime.Today;
            ModifiedDate = CreatedDate;
        }

        [RequiredField("AdministratorUsernameRequired", "The Username must be defined.")]
        [StringLength("AdministratorUsernameLength", "The Username must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("AdministratorEmailLength", "The Email must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("AdministratorPasswordLength", "The Password must have a length less than {1}", MaxLength = 100)]
        public string Password
        {
            get
            {
                return Data.Password;
            }
            set
            {
                Data.Password = value;
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

        [StringLength("AdministratorFullNameLength", "The FullName must have a length less than {1}", MaxLength = 100)]
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
