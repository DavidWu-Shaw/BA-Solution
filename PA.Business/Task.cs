using System;
using System.Collections.Generic;
using System.Text;
using Framework.Business;
using PA.Data;
using Framework.Core.Helpers;
using SubjectEngine.Business;
using PA.Core;

namespace PA.Business
{
    /// <summary>
    /// Summary description for ProjectTask.
    /// </summary>
    public class Task : ChildBusinessObject<Project, TaskData>
    {
        #region PROPERTIES
        public string Name
        {
            get { return Data.Name; }
            set
            {
                if (!IsDataValueEqual(Data.Name, (value)))
                {
                    Data.Name = value;
                    OnPropertyChanged("Name");

                    if (!IsNew)
                    {
                        string changeContent = HistoryHelper.ComposeHistoryContent("Name", Name);
                        AddChangeHistory(changeContent);
                    }
                }
            }
        }


        public string Description
        {
            get { return Data.Description; }
            set
            {
                if (!IsDataValueEqual(Data.Description, (value)))
                {
                    Data.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }



        public int? Duration
        {
            get { return Data.Duration; }
            set
            {
                if (!IsDataValueEqual(Data.Duration, (value)))
                {
                    Data.Duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public object OwnerId
        {
            get { return Data.OwnerId; }
            set
            {
                if (!IsDataValueEqual(Data.OwnerId, (value)))
                {
                    Data.OwnerId = value;
                    OnPropertyChanged("OwnerId");

                    if (!IsNew)
                    {
                        string changeContent;
                        if (OwnerId != null)
                        {
                            changeContent = HistoryHelper.ComposeHistoryContent("Owner", OwnerDisplay);
                        }
                        else
                        {
                            changeContent = HistoryHelper.ComposeHistoryContent("Owner");
                        }

                        AddChangeHistory(changeContent);
                    }
                }
            }
        }


        public DateTime? DueDate
        {
            get { return Data.DueDate; }
            set
            {
                if (!IsDataValueEqual(Data.DueDate, (value)))
                {
                    Data.DueDate = value;
                    OnPropertyChanged("DueDate");

                    if (!IsNew)
                    {
                        string changeContent;
                        if (DueDate.HasValue)
                        {
                            changeContent = HistoryHelper.ComposeHistoryContent("Due Date", DuedateDisplay);
                        }
                        else
                        {
                            changeContent = HistoryHelper.ComposeHistoryContent("Due Date");
                        }

                        AddChangeHistory(changeContent);
                    }
                }
            }
        }



        public DateTime? PlannedStartDate
        {
            get { return Data.PlannedStartDate; }
            set
            {
                if (!IsDataValueEqual(Data.PlannedStartDate, (value)))
                {
                    Data.PlannedStartDate = value;
                    OnPropertyChanged("PlannedStartDate");
                }
            }
        }



        public DateTime? ActualStartDate
        {
            get { return Data.ActualStartDate; }
            set
            {
                if (!IsDataValueEqual(Data.ActualStartDate, (value)))
                {
                    Data.ActualStartDate = value;
                    OnPropertyChanged("ActualStartDate");
                }
            }
        }



        public DateTime? ActualEndDate
        {
            get { return Data.ActualEndDate; }
            set
            {
                if (!IsDataValueEqual(Data.ActualEndDate, (value)))
                {
                    Data.ActualEndDate = value;
                    OnPropertyChanged("ActualEndDate");
                }
            }
        }


        public int? PhaseId
        {
            get { return Data.PhaseId; }
            set
            {
                if (!IsDataValueEqual(Data.PhaseId, (value)))
                {
                    Data.PhaseId = value;
                    OnPropertyChanged("PhaseId");
                }
            }
        }



        public int? PriorityId
        {
            get { return Data.PriorityId; }
            set
            {
                if (!IsDataValueEqual(Data.PriorityId, (value)))
                {
                    Data.PriorityId = value;
                    OnPropertyChanged("PriorityId");
                }
            }
        }



        public int? TimingDays
        {
            get { return Data.TimingDays; }
            set
            {
                if (!IsDataValueEqual(Data.TimingDays, (value)))
                {
                    Data.TimingDays = value;
                    OnPropertyChanged("TimingDays");
                }
            }
        }



        public int? TimingBaseTaskId
        {
            get { return Data.TimingBaseTaskId; }
            set
            {
                if (!IsDataValueEqual(Data.TimingBaseTaskId, (value)))
                {
                    Data.TimingBaseTaskId = value;
                    OnPropertyChanged("TimingBaseTaskId");
                }
            }
        }



        public int? StatusId
        {
            get { return Data.StatusId; }
            set
            {
                if (!IsDataValueEqual(Data.StatusId, (value)))
                {
                    Data.StatusId = value;
                    OnPropertyChanged("StatusId");

                    if (!IsNew)
                    {
                        string changeContent = string.Empty;
                        if (StatusId.HasValue)
                        {
                            changeContent = HistoryHelper.ComposeHistoryContent("Status", StatusDisplay);
                        }

                        AddChangeHistory(changeContent);
                    }
                }
            }
        }



        public int? ToleranceDays
        {
            get { return Data.ToleranceDays; }
            set
            {
                if (!IsDataValueEqual(Data.ToleranceDays, (value)))
                {
                    Data.ToleranceDays = value;
                    OnPropertyChanged("ToleranceDays");
                }
            }
        }



        public DateTime? ModelStartDate
        {
            get { return Data.ModelStartDate; }
            set
            {
                if (!IsDataValueEqual(Data.ModelStartDate, (value)))
                {
                    Data.ModelStartDate = value;
                    OnPropertyChanged("ModelStartDate");
                }
            }
        }



        public DateTime? ModelEndDate
        {
            get { return Data.ModelEndDate; }
            set
            {
                if (!IsDataValueEqual(Data.ModelEndDate, (value)))
                {
                    Data.ModelEndDate = value;
                    OnPropertyChanged("ModelEndDate");
                }
            }
        }



        public int? TypeId
        {
            get { return Data.TypeId; }
            set
            {
                if (!IsDataValueEqual(Data.TypeId, (value)))
                {
                    Data.TypeId = value;
                    OnPropertyChanged("TypeId");
                }
            }
        }

        public int? TimeRemain
        {
            get { return Data.TimeRemain; }
            set
            {
                if (!IsDataValueEqual(Data.TimeRemain, (value)))
                {
                    Data.TimeRemain = value;
                    OnPropertyChanged("TimeRemain");
                }
            }
        }



        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        [ChildCollection]
        public ChildBoCollection<TaskData, TaskTime, TaskTimeData> TaskTimes
        {
            get;
            private set;
        }

        [ChildCollection]
        public ChildBoCollection<TaskData, TaskComment, TaskCommentData> TaskComments
        {
            get;
            private set;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //TaskTimes = new ChildBoCollection<TaskData, TaskTime, TaskTimeData>
            //    (Service, Data.TaskTimesData, this);

            //TaskComments = new ChildBoCollection<TaskData, TaskComment, TaskCommentData>
            //    (Service, Data.TaskCommentsData, this);
        }

        #endregion CHILD COLLECTIONS

        //private SingleBOCache<WebUser> _webUser = new SingleBOCache<WebUser>(typeof(IWebUserService));

        //public Project Project
        //{
        //    get
        //    {
        //        return Parent;
        //    }
        //}

        //public int ProjectId
        //{
        //    get
        //    {
        //        return Project.Id;
        //    }
        //}

        //public string ProjectDisplay
        //{
        //    get
        //    {
        //        return Project.Name;
        //    }
        //}

        //public WebUser Owner
        //{
        //    get
        //    {
        //        if (OwnerId.HasValue)
        //        {
        //            return _webUser.Get(OwnerId);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        public string OwnerDisplay
        {
            get
            {
        //        if (Owner != null)
        //        {
        //            return Owner.Display;
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
                return string.Empty;
            }
        }

        //public string TypeDisplay
        //{
        //    get
        //    {
        //        if (TypeId.HasValue)
        //        {
        //            return ((TaskType)TypeId).ToString();
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}

        public string StatusDisplay
        {
            get
            {
                if (StatusId.HasValue)
                {
                    return ((TaskStatus)StatusId.Value).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string DuedateDisplay
        {
            get
            {
                if (DueDate.HasValue)
                {
                    return DueDate.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        //public TaskComment GetTaskComment(int taskCommentId)
        //{
        //    foreach (TaskComment taskComment in TaskComments)
        //    {
        //        if (taskComment.Id == taskCommentId)
        //        {
        //            return taskComment;
        //        }
        //    }

        //    return null;
        //}

        //public TaskTime GetTaskTime(int taskTimeId)
        //{
        //    foreach (TaskTime taskTime in TaskTimes)
        //    {
        //        if (taskTime.Id == taskTimeId)
        //        {
        //            return taskTime;
        //        }
        //    }

        //    return null;
        //}

        //# region Permission rules

        //public TaskPermissionLevel GetPermissionLevel(WebUser user)
        //{
        //    if (AllowAdmin(user))
        //    {
        //        return TaskPermissionLevel.Admin;
        //    }
        //    else if (AllowChange(user))
        //    {
        //        return TaskPermissionLevel.Change;
        //    }
        //    else
        //    {
        //        return TaskPermissionLevel.Read;
        //    }
        //}

        //public bool IsOwner(WebUser user)
        //{
        //    return OwnerId.HasValue && OwnerId.Value == user.Id;
        //}

        //public bool AllowAdmin(WebUser user)
        //{
        //    return Project.AllowAdmin(user);
        //}

        //public bool AllowChange(WebUser user)
        //{
        //    return IsOwner(user) || AllowAdmin(user);
        //}

        //public bool AllowDelete(WebUser user)
        //{
        //    return false;
        //}

        //# endregion permission check

        //# region Status and State

        //public bool IsCompleted
        //{
        //    get
        //    {
        //        return StatusId.HasValue && StatusId.Value == (int)TaskStatus.Completed;
        //    }
        //}

        //public bool IsScheduled
        //{
        //    get
        //    {
        //        return DueDate.HasValue;
        //    }
        //}

        //public bool IsOverdue
        //{
        //    get
        //    {
        //        if (DueDate.HasValue && !IsCompleted)
        //        {
        //            if (DateTime.Today.Date > DueDate.Value.Date)
        //            {
        //                return true;
        //            }
        //        }

        //        return false;
        //    }
        //}

        //public int StateId
        //{
        //    get
        //    {
        //        if (IsCompleted)
        //        {
        //            return (int)TaskState.Completed;
        //        }
        //        else if (IsOverdue)
        //        {
        //            return (int)TaskState.Overdue;
        //        }
        //        else
        //        {
        //            return (int)TaskState.Normal;
        //        }
        //    }
        //}

        //public string StateDisplay
        //{
        //    get
        //    {
        //        string stateDisplay = GetStateDisplay(StateId);
        //        if (StateId == (int)TaskState.Overdue)
        //        {
        //            stateDisplay = string.Format("{0} ({1} days)", stateDisplay, OverdueDays);
        //        }

        //        return stateDisplay;
        //    }
        //}

        //public Color StateColor
        //{
        //    get
        //    {
        //        return GetStateColor(StateId);
        //    }
        //}

        //public static Color GetStateColor(int stateId)
        //{
        //    if (stateId == (int)TaskState.Completed)
        //    {
        //        return CommonDef.CompletedTaskColor;
        //    }
        //    else if (stateId == (int)TaskState.Overdue)
        //    {
        //        return CommonDef.OverdueTaskColor;
        //    }
        //    else
        //    {
        //        return CommonDef.NormalTaskColor;
        //    }
        //}

        //public static string GetStateDisplay(int stateId)
        //{
        //    return ((TaskState)stateId).ToString();
        //}

        ////TODO: need to test
        //public int? RemainingDays
        //{
        //    get
        //    {
        //        if (DueDate.HasValue && !IsCompleted)
        //        {
        //            TimeSpan ts = DueDate.Value - DateTime.Today;
        //            return ts.Days;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        ////TODO: need to test
        //public int? OverdueDays
        //{
        //    get
        //    {
        //        if (IsOverdue)
        //        {
        //            TimeSpan ts = DateTime.Today - DueDate.Value;
        //            return ts.Days;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //# endregion Status and State

        //#region Change history log

        //public List<ChangeHistory> ChangeHistorys
        //{
        //    get
        //    {
        //        return Project.ChangeHistorys;
        //    }
        //}

        public void AddChangeHistory(string changeContent)
        {
        //    ChangeHistory changeHistory = AppContext.Current.GetService<IChangeHistoryService>().GetNew();
        //    changeHistory.ChangeContent = changeContent;
        //    changeHistory.ObjectTypeId = (int)ObjectType.Task;
        //    changeHistory.ObjectId = this.Id;
        //    changeHistory.ObjectParentId = Project.Id;

        //    ChangeHistorys.Add(changeHistory);
        }

        //#endregion        

    }
}
