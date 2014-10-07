using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for DocumentData.
    /// </summary>
    public class DocumentData : ChildDataObject
    {        
        public DocumentData()
        {
        }

        #region PROPERTIES

		public virtual string Code { get; set; } 

		public virtual byte[] Content { get; set; } 

		public virtual int? PublishedMode { get; set; }

        public virtual byte[] Thumbnail { get; set; } 

		public virtual string Notes { get; set; } 

		public virtual int? TypeId { get; set; } 

		public virtual object IssuedById { get; set; } 

		public virtual DateTime? IssuedDate { get; set; } 

		public virtual string Source { get; set; } 

		public virtual string Description { get; set; } 

		public virtual int? OriginalCopyId { get; set; } 

		public virtual string Extension { get; set; } 


        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        #endregion CHILD COLLECTIONS

    }
}
