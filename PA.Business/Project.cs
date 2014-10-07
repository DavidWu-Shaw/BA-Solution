using System;
using System.Collections.Generic;
using Framework.Business;
using PA.Data;
using PA.Core;

namespace PA.Business
{
    public class Project : BusinessObject<ProjectData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            ProjectTasks = new ChildBoCollection<ProjectData, Task, TaskData>
                (Service, Data.ProjectTasksData, this);

            ProjectMembers = new ChildBoCollection<ProjectData, ProjectMember, ProjectMemberData>
                (Service, Data.ProjectMembersData, this);

            ProjectDocuments = new ChildBoCollection<ProjectData, ProjectDocument, ProjectDocumentData>
                (Service, Data.ProjectDocumentsData, this);

            //IssuedByUser = new LazyBoProperty<WebUser, IWebUserService>(UnitOfWork,
            //    () => Data.IssuedByUserData,
            //    value => Data.IssuedByUserData = value.Cast<WebUserData>());
        }

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
                }
            }
        }

        public string Code
        {
            get { return Data.Code; }
            set
            {
                if (!IsDataValueEqual(Data.Code, (value)))
                {
                    Data.Code = value;
                    OnPropertyChanged("Code");
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

        public object IssuedById
        {
            get { return Data.IssuedById; }
            set
            {
                if (!IsDataValueEqual(Data.IssuedById, (value)))
                {
                    Data.IssuedById = value;
                    OnPropertyChanged("IssuedById");
                }
            }
        }


        public DateTime IssuedDate
        {
            get { return Data.IssuedDate; }
            set
            {
                if (!IsDataValueEqual(Data.IssuedDate, (value)))
                {
                    Data.IssuedDate = value;
                    OnPropertyChanged("IssuedDate");
                }
            }
        }

        public object ParentProjectId
        {
            get { return Data.ParentProjectId; }
            set
            {
                if (!IsDataValueEqual(Data.ParentProjectId, (value)))
                {
                    Data.ParentProjectId = value;
                    OnPropertyChanged("ParentProjectId");
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



        public DateTime? PlannedEndDate
        {
            get { return Data.PlannedEndDate; }
            set
            {
                if (!IsDataValueEqual(Data.PlannedEndDate, (value)))
                {
                    Data.PlannedEndDate = value;
                    OnPropertyChanged("PlannedEndDate");
                }
            }
        }

        public int? TargetTypeId
        {
            get { return Data.TargetTypeId; }
            set
            {
                if (!IsDataValueEqual(Data.TargetTypeId, (value)))
                {
                    Data.TargetTypeId = value;
                    OnPropertyChanged("TargetTypeId");
                }
            }
        }

        public object TargetId
        {
            get { return Data.TargetId; }
            set
            {
                if (!IsDataValueEqual(Data.TargetId, (value)))
                {
                    Data.TargetId = value;
                    OnPropertyChanged("TargetId");
                }
            }
        }

        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        [ChildCollection]
        public ChildBoCollection<ProjectData, Task, TaskData> ProjectTasks
        {
            get;
            private set;
        }

        [ChildCollection]
        public ChildBoCollection<ProjectData, ProjectMember, ProjectMemberData> ProjectMembers
        {
            get;
            private set;
        }

        [ChildCollection]
        public ChildBoCollection<ProjectData, ProjectDocument, ProjectDocumentData> ProjectDocuments
        {
            get;
            private set;
        }

        #endregion CHILD COLLECTIONS

        //protected override void OnAfterSave()
        //{
        //    base.OnAfterSave();

        //    if (IsNewCreated)
        //    {
        //        AddChangeHistory("created");
        //    }

        //    SaveChangeHistorys();
        //}

        # region Business logic

        //public LazyBoProperty<WebUser, IWebUserService> IssuedByUser
        //{
        //    get;
        //    private set;
        //}


        //public string CreatorDisplay
        //{
        //    get
        //    {
        //        return IssuedByUser.Value.Display;
        //    }
        //}

        //public string CreatedDateDisplay
        //{
        //    get
        //    {
        //        return IssuedDate.ToShortDateString();
        //    }
        //}

        //public string CreatedByInfo
        //{
        //    get
        //    {
        //        return string.Format("Created by {0}", CreatorDisplay);
        //    }
        //}

        //private User[] _members;
        //public User[] Members
        //{
        //    get
        //    {
        //        if (_members == null)
        //        {
        //            List<User> members = new List<User>();

        //            foreach (ProjectMember projectMember in ProjectMembers)
        //            {
        //                //members.Add(_webUser.Get(projectMember.MemberId));
        //            }

        //            _members = members.ToArray();
        //        }

        //        return _members;
        //    }
        //}

        public Task GetTask(object taskId)
        {
            foreach (Task task in this.ProjectTasks)
            {
                if (task.Id == taskId)
                {
                    return task;
                }
            }

            return null;
        }

        //public bool CompleteTask(Task task, User user)
        //{
        //    task.StatusId = (int)TaskStatus.Completed;
        //    task.ActualEndDate = DateTime.Today;

        //    IServiceUpdateResult result = this.Save();

        //    if (result.IsSuccessful)
        //    {
        //        this.SaveChangeHistorys();
        //    }

        //    return result.IsSuccessful;
        //}

        public bool DeleteTaskByTaskId(int taskId)
        {
            bool isSuccessful = false;

            Task task = GetTask(taskId);
            if (task != null)
            {
                this.ProjectTasks.Remove(task);
                isSuccessful = this.Save().IsSuccessful;
            }

            return isSuccessful;
        }

        public bool DeleteProjectMemberById(int projectMemberId)
        {
            bool isSuccessful = false;

            ProjectMember projectMember = GetProjectMember(projectMemberId);
            if (projectMember != null)
            {
                this.ProjectMembers.Remove(projectMember);
                isSuccessful = this.Save().IsSuccessful;
            }

            return isSuccessful;
        }

        /// <summary>
        /// Gets the ProjectMember object by id
        /// </summary>
        /// <param name="projectMemberId"></param>
        /// <returns> If the id is a valid one of this project, return the ProjectMember object;
        ///     else return null
        /// </returns>
        public ProjectMember GetProjectMember(object projectMemberId)
        {
            foreach (ProjectMember projectMember in ProjectMembers)
            {
                if (projectMember.Id == projectMemberId)
                {
                    return projectMember;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the ProjectMember object of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> If the user is a member of current project, return the ProjectMember object;
        ///     else return null
        /// </returns>
        public ProjectMember GetProjectMember(User user)
        {
            foreach (ProjectMember projectMember in ProjectMembers)
            {
                if (projectMember.MemberId == user.Id)
                {
                    return projectMember;
                }
            }

            return null;
        }

        # region Permission check

        public ProjectPermissionLevel GetUserPermissionLevel(User user)
        {
            if (AllowAdmin(user))
            {
                return ProjectPermissionLevel.Admin;
            }
            else if (AllowEdit(user))
            {
                return ProjectPermissionLevel.Edit;
            }
            else
            {
                return ProjectPermissionLevel.Read;
            }
        }

        /// <summary>
        /// Get read permission for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> If user is a member of current project, return true; else return false.</returns>
        public bool AllowRead(User user)
        {
            if (user.IsAdmin)
            {
                return true;
            }

            ProjectMember projectMember = GetProjectMember(user);
            if (projectMember != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllowEdit(User user)
        {
            if (user.IsAdmin)
            {
                return true;
            }

            ProjectMember projectMember = GetProjectMember(user);
            if (projectMember == null)
            {
                return false;
            }
            else
            {
                return projectMember.AllowEdit;
            }
        }

        public bool AllowAdmin(User user)
        {
            if (user.IsAdmin)
            {
                return true;
            }

            ProjectMember projectMember = GetProjectMember(user);
            if (projectMember == null)
            {
                return false;
            }
            else
            {
                return projectMember.AllowAdmin;
            }
        }

        # endregion Permission check

        # endregion Business logic

        #region Log change history

        private List<ChangeHistory> _changeHistorys;
        public List<ChangeHistory> ChangeHistorys
        {
            get
            {
                if (_changeHistorys == null)
                {
                    _changeHistorys = new List<ChangeHistory>();
                }

                return _changeHistorys;
            }
        }

        public void AddChangeHistory(string changeContent)
        {
            //ChangeHistory changeHistory = UnitOfWork.GetService<IChangeHistoryService>().CreateNew<ChangeHistory>();
            //changeHistory.ChangeContent = changeContent;
            ////changeHistory.ObjectTypeId = (int)ObjectType.Project;
            //changeHistory.ObjectId = (int)this.Id;

            //ChangeHistorys.Add(changeHistory);
        }

        public void SaveChangeHistorys()
        {
            foreach (ChangeHistory change in ChangeHistorys)
            {
                //change.ChangedById = AppContext.Current.CurrentUserId;
                change.ChangedDate = DateTime.Now;
                change.Save();
            }
        }

        #endregion
    }
}
