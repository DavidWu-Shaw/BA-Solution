using System;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for ChangeHistoryData.
    /// </summary>
    public class ChangeHistoryData : DataObject
    {        
        public ChangeHistoryData()
        {
        }

        #region PROPERTIES

		public virtual string ChangeContent { get; set; } 

		public virtual int ObjectTypeId { get; set; }

        public virtual int ObjectId { get; set; }
        public virtual int? ObjectParentId { get; set; } 

		public virtual DateTime ChangedDate { get; set; } 

		public virtual int ChangedById { get; set; } 


        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        #endregion CHILD COLLECTIONS

    }
}
