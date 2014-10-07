using System;
using System.Collections.Generic;
using System.Text;
using Framework.Business;
using PA.Data;

namespace PA.Business
{
    /// <summary>
    /// Summary description for ProjectDocument.
    /// </summary>
    public class ProjectDocument : ChildBusinessObject<Project, ProjectDocumentData>
    {        
        #region PROPERTIES

        public object DocumentId
		{
			get { return Data.DocumentId; }
			set
			{
				if ( !IsDataValueEqual( Data.DocumentId, ( value ) ) )
				{
					Data.DocumentId = value;
					OnPropertyChanged( "DocumentId" );
				}
			}
		}



        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        #endregion CHILD COLLECTIONS


    }
}
