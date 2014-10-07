using System;
using System.Collections.Generic;
using System.Text;
using Framework.Business;
using PA.Data;

namespace PA.Business
{
    /// <summary>
    /// Summary description for TaskTime.
    /// </summary>
    public class TaskTime : ChildBusinessObject<Task, TaskTimeData>
    {
        public object UserId
		{
			get { return Data.UserId; }
			set
			{
				if ( !IsDataValueEqual( Data.UserId, ( value ) ) )
				{
					Data.UserId = value;
					OnPropertyChanged( "UserId" );
				}
			}
		}


		
		public int TimeSpent
		{
			get { return Data.TimeSpent; }
			set
			{
				if ( !IsDataValueEqual( Data.TimeSpent, ( value ) ) )
				{
					Data.TimeSpent = value;
					OnPropertyChanged( "TimeSpent" );
				}
			}
		}


		
		public DateTime DateSpent
		{
			get { return Data.DateSpent; }
			set
			{
				if ( !IsDataValueEqual( Data.DateSpent, ( value ) ) )
				{
					Data.DateSpent = value;
					OnPropertyChanged( "DateSpent" );
				}
			}
		}


		public string Note
		{
			get { return Data.Note; }
			set
			{
				if ( !IsDataValueEqual( Data.Note, ( value ) ) )
				{
					Data.Note = value;
					OnPropertyChanged( "Note" );
				}
			}
		}


		
		public DateTime? TimeFrom
		{
			get { return Data.TimeFrom; }
			set
			{
				if ( !IsDataValueEqual( Data.TimeFrom, ( value ) ) )
				{
					Data.TimeFrom = value;
					OnPropertyChanged( "TimeFrom" );
				}
			}
		}


		
		public DateTime? TimeTo
		{
			get { return Data.TimeTo; }
			set
			{
				if ( !IsDataValueEqual( Data.TimeTo, ( value ) ) )
				{
					Data.TimeTo = value;
					OnPropertyChanged( "TimeTo" );
				}
			}
		}


		
		public decimal? Rate
		{
			get { return Data.Rate; }
			set
			{
				if ( !IsDataValueEqual( Data.Rate, ( value ) ) )
				{
					Data.Rate = value;
					OnPropertyChanged( "Rate" );
				}
			}
		}


        //private SingleBOCache<WebUser> _webUser = new SingleBOCache<WebUser>(typeof(IWebUserService));

        //public WebUser TimeUser
        //{
        //    get
        //    {
        //        return _webUser.Get(UserId);
        //    }
        //}

        //public string TimeUserDisplay
        //{
        //    get
        //    {
        //        return TimeUser.Display;
        //    }
        //}

        //public string TaskName
        //{
        //    get
        //    {
        //        return Parent.Name;
        //    }
        //}

        public string DateSpentDisplay
        {
            get
            {
                return DateSpent.ToShortDateString();
            }
        }
    }
}
