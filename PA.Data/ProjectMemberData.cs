using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for ProjectMemberData.
    /// </summary>
    public class ProjectMemberData : ChildDataObject
    {        
        public ProjectMemberData()
        {
        }

        #region PROPERTIES

        public virtual UserData MemberData { get; set; } 

		public virtual int? MemberRoleId { get; set; } 

		public virtual bool? AllowRead { get; set; } 

		public virtual bool AllowEdit { get; set; } 

		public virtual bool? AllowAdd { get; set; } 

		public virtual bool? AllowDelete { get; set; } 

		public virtual bool AllowAdmin { get; set; } 

        #endregion PROPERTIES

    }
}
