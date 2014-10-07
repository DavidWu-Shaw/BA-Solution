using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for TaskTimeData.
    /// </summary>
    public class TaskTimeData : ChildDataObject
    {
        public TaskTimeData()
        {
        }

        public virtual object UserId { get; set; } 

		public virtual int TimeSpent { get; set; } 

		public virtual DateTime DateSpent { get; set; } 

		public virtual string Note { get; set; } 

		public virtual DateTime? TimeFrom { get; set; } 

		public virtual DateTime? TimeTo { get; set; } 

		public virtual decimal? Rate { get; set; } 

    }
}
