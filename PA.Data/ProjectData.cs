using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// </summary>
    public class ProjectData : DataObject
    {        
        public ProjectData()
        {
        }

        public virtual string Code { get; set; }
        public virtual string Name { get; set; } 

		public virtual string Description { get; set; }

        public virtual object IssuedById { get; set; }
        public virtual DateTime IssuedDate { get; set; }

        public virtual object ParentProjectId { get; set; }

        public virtual DateTime? ModelStartDate { get; set; }
        public virtual DateTime? ModelEndDate { get; set; }
        public virtual DateTime? PlannedStartDate { get; set; }
        public virtual DateTime? PlannedEndDate { get; set; } 

		public virtual int? TargetTypeId { get; set; }

        public virtual object TargetId { get; set; }


        private IList<ProjectMemberData> _projectMembersData = new List<ProjectMemberData>();
        public virtual IList<ProjectMemberData> ProjectMembersData
		{
			get { return _projectMembersData; }
			set { _projectMembersData = value; }
		}

        private IList<ProjectDocumentData> _projectDocumentsData = new List<ProjectDocumentData>();
        public virtual IList<ProjectDocumentData> ProjectDocumentsData
		{
			get { return _projectDocumentsData; }
			set { _projectDocumentsData = value; }
		}

        private IList<TaskData> _projectTasksData = new List<TaskData>();
		public virtual IList<TaskData> ProjectTasksData
		{
			get { return _projectTasksData; }
			set { _projectTasksData = value; }
		}
    }
}
