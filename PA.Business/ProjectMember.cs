using System;
using Framework.Business;
using PA.Data;
using PA.Service.Contract;
using Framework.Core;

namespace PA.Business
{
    /// <summary>
    /// Summary description for ProjectMember.
    /// </summary>
    public class ProjectMember : ChildBusinessObject<Project, ProjectMemberData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Member = new LazyBoProperty<User, IUserService>(UnitOfWork,
                () => Data.MemberData,
                value => Data.MemberData = value.Cast<UserData>());
               
        }

        public LazyProperty<User> Member
        {
            get;
            private set;
        }
		
		public int? MemberRoleId
		{
			get { return Data.MemberRoleId; }
			set
			{
				if ( !IsDataValueEqual( Data.MemberRoleId, ( value ) ) )
				{
					Data.MemberRoleId = value;
					OnPropertyChanged( "MemberRoleId" );
				}
			}
		}


		
		public bool? AllowRead
		{
			get { return Data.AllowRead; }
			set
			{
				if ( !IsDataValueEqual( Data.AllowRead, ( value ) ) )
				{
					Data.AllowRead = value;
					OnPropertyChanged( "AllowRead" );
				}
			}
		}


		
		public bool AllowEdit
		{
			get { return Data.AllowEdit; }
			set
			{
				if ( !IsDataValueEqual( Data.AllowEdit, ( value ) ) )
				{
					Data.AllowEdit = value;
					OnPropertyChanged( "AllowEdit" );
				}
			}
		}


		
		public bool? AllowAdd
		{
			get { return Data.AllowAdd; }
			set
			{
				if ( !IsDataValueEqual( Data.AllowAdd, ( value ) ) )
				{
					Data.AllowAdd = value;
					OnPropertyChanged( "AllowAdd" );
				}
			}
		}


		
		public bool? AllowDelete
		{
			get { return Data.AllowDelete; }
			set
			{
				if ( !IsDataValueEqual( Data.AllowDelete, ( value ) ) )
				{
					Data.AllowDelete = value;
					OnPropertyChanged( "AllowDelete" );
				}
			}
		}


		
		public bool AllowAdmin
		{
			get { return Data.AllowAdmin; }
			set
			{
				if ( !IsDataValueEqual( Data.AllowAdmin, ( value ) ) )
				{
					Data.AllowAdmin = value;
					OnPropertyChanged( "AllowAdmin" );
				}
			}
		}

        # region Data binding properties

        public object MemberId
        {
            get
            {
                return Member.Value.Id;
            }
        }

        public string MemberName
        {
            get
            {
                return Member.Value.Username;
            }
        }

        public string MemberEmail
        {
            get
            {
                return Member.Value.Email;
            }
        }

        # endregion 

    }
}
