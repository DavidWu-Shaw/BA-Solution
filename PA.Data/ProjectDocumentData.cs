using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for ProjectDocumentData.
    /// </summary>
    public class ProjectDocumentData : ChildDataObject
    {        
        public ProjectDocumentData()
        {
        }

        #region PROPERTIES

		public virtual object DocumentId { get; set; } 

        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        #endregion CHILD COLLECTIONS

    }
}
