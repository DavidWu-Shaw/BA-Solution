using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Framework.Business;
using PA.Data;
using Framework.Core.Helpers;
using Framework.Security;
using SubjectEngine.Business;
using SubjectEngine.Data;

namespace PA.Business
{
    public class User : BusinessObject<UserData>
    {        
        #region PROPERTIES
        public string Username
        {
            get { return Data.Username; }
            set
            {
                if (!IsDataValueEqual(Data.Username, (value)))
                {
                    Data.Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }


        public string Email
		{
			get { return Data.Email; }
			set
			{
				if ( !IsDataValueEqual( Data.Email, ( value ) ) )
				{
					Data.Email = value;
					OnPropertyChanged( "Email" );
				}
			}
		}


		
		public int StatusId
		{
			get { return Data.StatusId; }
			set
			{
				if ( !IsDataValueEqual( Data.StatusId, ( value ) ) )
				{
					Data.StatusId = value;
					OnPropertyChanged( "StatusId" );
				}
			}
		}


        public string FullName
        {
            get { return Data.FullName; }
            set
            {
                if (!IsDataValueEqual(Data.FullName, (value)))
                {
                    Data.FullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

		
		public int? DomainId
		{
			get { return Data.DomainId; }
			set
			{
				if ( !IsDataValueEqual( Data.DomainId, ( value ) ) )
				{
					Data.DomainId = value;
					OnPropertyChanged( "DomainId" );
				}
			}
		}


		
		public DateTime? CreateDate
		{
			get { return Data.CreateDate; }
			set
			{
				if ( !IsDataValueEqual( Data.CreateDate, ( value ) ) )
				{
					Data.CreateDate = value;
					OnPropertyChanged( "CreateDate" );
				}
			}
		}


		
		public DateTime? UpdateDate
		{
			get { return Data.UpdateDate; }
			set
			{
				if ( !IsDataValueEqual( Data.UpdateDate, ( value ) ) )
				{
					Data.UpdateDate = value;
					OnPropertyChanged( "UpdateDate" );
				}
			}
		}


		
		public int? MenuSettingId
		{
            get { return Data.MenuSettingId; }
			set
			{
                if (!IsDataValueEqual(Data.MenuSettingId, (value)))
				{
                    Data.MenuSettingId = value;
                    OnPropertyChanged("MenuSettingId");
				}
			}
		}


		
		public int? UserLanguage
		{
			get { return Data.UserLanguage; }
			set
			{
				if ( !IsDataValueEqual( Data.UserLanguage, ( value ) ) )
				{
					Data.UserLanguage = value;
					OnPropertyChanged( "UserLanguage" );
				}
			}
		}


		
		public int? Theme
		{
			get { return Data.Theme; }
			set
			{
				if ( !IsDataValueEqual( Data.Theme, ( value ) ) )
				{
					Data.Theme = value;
					OnPropertyChanged( "Theme" );
				}
			}
		}


		public string Password
		{
			get { return Data.Password; }
			set
			{
				if ( !IsDataValueEqual( Data.Password, ( value ) ) )
				{
					Data.Password = value;
					OnPropertyChanged( "Password" );
				}
			}
		}


		
		public bool? IsBuiltInUser
		{
			get { return Data.IsBuiltInUser; }
			set
			{
				if ( !IsDataValueEqual( Data.IsBuiltInUser, ( value ) ) )
				{
					Data.IsBuiltInUser = value;
					OnPropertyChanged( "IsBuiltInUser" );
				}
			}
		}


		
		public bool? IsActive
		{
			get { return Data.IsActive; }
			set
			{
				if ( !IsDataValueEqual( Data.IsActive, ( value ) ) )
				{
					Data.IsActive = value;
					OnPropertyChanged( "IsActive" );
				}
			}
		}


		
		public int? LanguageId
		{
			get { return Data.LanguageId; }
			set
			{
				if ( !IsDataValueEqual( Data.LanguageId, ( value ) ) )
				{
					Data.LanguageId = value;
					OnPropertyChanged( "LanguageId" );
				}
			}
		}


		
		public int? CalendarId
		{
			get { return Data.CalendarId; }
			set
			{
				if ( !IsDataValueEqual( Data.CalendarId, ( value ) ) )
				{
					Data.CalendarId = value;
					OnPropertyChanged( "CalendarId" );
				}
			}
		}



        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        #endregion CHILD COLLECTIONS

        #region IUser Members

        //int IUser.Id
        //{
        //    get { return Id; }
        //}

        //string IUser.LoginName
        //{
        //    get { return LoginName; }
        //}

        //string IUser.Password
        //{
        //    get { return string.Empty; }
        //}

        //string IUser.DisplayName
        //{
        //    get { return Display; }
        //}

        #endregion

        # region Authentication and Authorization

        public bool IsAdmin
        {
            get
            {
                return Username.Equals("admin");
            }
        }

        public bool VerifyPassword(string inputPassword)
        {
            string input = SecurityHelper.Encrypt(inputPassword);
            return input == Password;
        }

        # endregion Authentication and Authorization        
    }
}
