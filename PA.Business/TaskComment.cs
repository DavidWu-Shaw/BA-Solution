using System;
using System.Collections.Generic;
using System.Text;
using Framework.Business;
using PA.Data;

namespace PA.Business
{
    /// <summary>
    /// Summary description for TaskComment.
    /// </summary>
    public class TaskComment : ChildBusinessObject<Task, TaskCommentData>
    {        
		public string Comment
		{
			get { return Data.Comment; }
			set
			{
				if ( !IsDataValueEqual( Data.Comment, ( value ) ) )
				{
					Data.Comment = value;
					OnPropertyChanged( "Comment" );
				}
			}
		}


		
		public DateTime IssuedDate
		{
			get { return Data.IssuedDate; }
			set
			{
				if ( !IsDataValueEqual( Data.IssuedDate, ( value ) ) )
				{
					Data.IssuedDate = value;
					OnPropertyChanged( "IssuedDate" );
				}
			}
		}



        public object IssuedById
		{
			get { return Data.IssuedById; }
			set
			{
                if (!IsDataValueEqual(Data.IssuedById, (value)))
				{
                    Data.IssuedById = value;
                    OnPropertyChanged("IssuedById");
				}
			}
		}

        #region binding property

        //private SingleBOCache<WebUser> _webUser = new SingleBOCache<WebUser>(typeof(IWebUserService));

        //public WebUser IssuedByUser
        //{
        //    get
        //    {
        //        return _webUser.Get(IssuedBy);
        //    }
        //}

        //public string IssuedByDisplay
        //{
        //    get
        //    {
        //        return IssuedByUser.Display;
        //    }
        //}

        //public string IssuedDateDisplay
        //{
        //    get
        //    {
        //        return IssuedDate.ToString(CommonDef.DateTimeFormat);
        //    }
        //}

        #endregion
    }
}
