using System;
using System.Collections.Generic;
using System.Text;
using Framework.Business;
using PA.Data;

namespace PA.Business
{
    /// <summary>
    /// Summary description for ChangeHistory.
    /// </summary>
    public class ChangeHistory : BusinessObject<ChangeHistoryData>
    {        
        #region PROPERTIES
		public string ChangeContent
		{
			get { return Data.ChangeContent; }
			set
			{
				if ( !IsDataValueEqual( Data.ChangeContent, ( value ) ) )
				{
					Data.ChangeContent = value;
					OnPropertyChanged( "ChangeContent" );
				}
			}
		}


		
		public int ObjectTypeId
		{
			get { return Data.ObjectTypeId; }
			set
			{
				if ( !IsDataValueEqual( Data.ObjectTypeId, ( value ) ) )
				{
					Data.ObjectTypeId = value;
					OnPropertyChanged( "ObjectTypeId" );
				}
			}
		}



        public int ObjectId
        {
            get { return Data.ObjectId; }
            set
            {
                if (!IsDataValueEqual(Data.ObjectId, (value)))
                {
                    Data.ObjectId = value;
                    OnPropertyChanged("ObjectId");
                }
            }
        }

        public int? ObjectParentId
        {
            get { return Data.ObjectParentId; }
            set
            {
                if (!IsDataValueEqual(Data.ObjectParentId, (value)))
                {
                    Data.ObjectParentId = value;
                    OnPropertyChanged("ObjectParentId");
                }
            }
        }


		
		public DateTime ChangedDate
		{
			get { return Data.ChangedDate; }
			set
			{
				if ( !IsDataValueEqual( Data.ChangedDate, ( value ) ) )
				{
					Data.ChangedDate = value;
					OnPropertyChanged( "ChangedDate" );
				}
			}
		}



        public int ChangedById
		{
			get { return Data.ChangedById; }
			set
			{
                if (!IsDataValueEqual(Data.ChangedById, (value)))
				{
                    Data.ChangedById = value;
                    OnPropertyChanged("ChangedById");
				}
			}
		}



        #endregion PROPERTIES

        //private SingleBOCache<WebUser> _webUser = new SingleBOCache<WebUser>(typeof(IWebUserService));
        //private SingleBOCache<Project> _project = new SingleBOCache<Project>(typeof(IProjectService));

        //public string ChangedByDisplay
        //{
        //    get
        //    {
        //        return _webUser.Get(ChangedBy).Display;
        //    }
        //}

        //public ObjectType ObjectType
        //{
        //    get { return (ObjectType)ObjectTypeId; }
        //}

        //public string ObjectDisplay
        //{
        //    get
        //    {
        //        string objectDisplay = string.Empty;

        //        switch (ObjectType)
        //        {
        //            case ObjectType.User:
        //                objectDisplay = _webUser.Get(ObjectId).Display;
        //                break;
        //            case ObjectType.Project:
        //                objectDisplay = _project.Get(ObjectId).Name;
        //                break;
        //            case ObjectType.Task:
        //                Project project = _project.Get(ObjectParentId);
        //                Task task = project[ObjectId];
        //                objectDisplay = string.Format("{0} ({1})", task.Name, project.Name);
        //                    break;
        //        }

        //        return objectDisplay;
        //    }
        //}

        //public string ChangedDateDisplay
        //{
        //    get
        //    {
        //        return ChangedDate.ToString(CommonDef.DateTimeFormat);
        //    }
        //}
    }
}
