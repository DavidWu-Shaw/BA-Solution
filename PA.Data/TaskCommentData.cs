using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for TaskCommentData.
    /// </summary>
    public class TaskCommentData : ChildDataObject
    {        
        public TaskCommentData()
        {
        }

		public virtual string Comment { get; set; } 

		public virtual DateTime IssuedDate { get; set; }

        public virtual object IssuedById { get; set; } 
    }
}
