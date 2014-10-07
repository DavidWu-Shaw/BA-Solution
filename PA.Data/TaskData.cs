using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Framework.Data;

namespace PA.Data
{
    /// <summary>
    /// Summary description for ProjectTaskData.
    /// </summary>
    public class TaskData : ChildDataObject
    {
        public TaskData()
        {
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual object OwnerId { get; set; }

        public virtual int? Duration { get; set; }

        public virtual DateTime? DueDate { get; set; }

        public virtual DateTime? PlannedStartDate { get; set; }

        public virtual DateTime? ActualStartDate { get; set; }

        public virtual DateTime? ActualEndDate { get; set; }

        public virtual int? PhaseId { get; set; }

        public virtual int? PriorityId { get; set; }

        public virtual int? TimingDays { get; set; }

        public virtual int? TimingBaseTaskId { get; set; }

        public virtual int? StatusId { get; set; }

        public virtual int? ToleranceDays { get; set; }

        public virtual DateTime? ModelStartDate { get; set; }

        public virtual DateTime? ModelEndDate { get; set; }

        public virtual int? TypeId { get; set; }

        public virtual int? TimeRemain { get; set; }

        private IList<TaskTimeData> _taskTimesData = new List<TaskTimeData>();
        public virtual IList<TaskTimeData> TaskTimesData
        {
            get { return _taskTimesData; }
            set { _taskTimesData = value; }
        }

        private IList<TaskCommentData> _taskCommentsData = new List<TaskCommentData>();
        public virtual IList<TaskCommentData> TaskCommentsData
        {
            get { return _taskCommentsData; }
            set { _taskCommentsData = value; }
        }

    }
}
