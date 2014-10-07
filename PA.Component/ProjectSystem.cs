using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using PA.Business;
using PA.Component.Dto;
using PA.Data;
using PA.Service.Contract;

namespace PA.Component
{
    internal class ProjectSystem : BusinessComponent
    {
        public ProjectSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllProject<TDto>(IDataConverter<ProjectData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IProjectService service = UnitOfWork.GetService<IProjectService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewProject<TDto>(object projectId, IDataConverter<ProjectData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IProjectService service = UnitOfWork.GetService<IProjectService>();
            FacadeUpdateResult<ProjectData> result = new FacadeUpdateResult<ProjectData>();
            Project project = RetrieveOrNew<ProjectData, Project, IProjectService>(result.ValidationResult, projectId);
            if (result.IsSuccessful)
            {
                return converter.Convert(project.RetrieveData<ProjectData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ProjectData> SaveProject(ProjectDto projectDto)
        {
            ArgumentValidator.IsNotNull("projectDto", projectDto);

            FacadeUpdateResult<ProjectData> result = new FacadeUpdateResult<ProjectData>();
            IProjectService projectService = UnitOfWork.GetService<IProjectService>();
            Project project = RetrieveOrNew<ProjectData, Project, IProjectService>(result.ValidationResult, projectDto.Id);

            if (result.IsSuccessful)
            {
                project.Code = projectDto.Code;
                project.Name = projectDto.Name;
                project.Description = projectDto.Description;
                project.IssuedById = 1; // projectDto.IssuedById;
                project.IssuedDate = DateTime.Now; // projectDto.IssuedDate;

                var saveQuery = projectService.Save(project);

                result.AttachResult(project.RetrieveData<ProjectData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ProjectData> DeleteProject(object projectId)
        {
            ArgumentValidator.IsNotNull("projectId", projectId);

            FacadeUpdateResult<ProjectData> result = new FacadeUpdateResult<ProjectData>();
            IProjectService service = UnitOfWork.GetService<IProjectService>();
            var query = service.Retrieve(projectId);
            if (query.HasResult)
            {
                Project project = query.ToBo<Project>();
                var saveQuery = project.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ProjectCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<ProjectData> DeleteTask(object childId, object parentId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<ProjectData> result = new FacadeUpdateResult<ProjectData>();
            IProjectService service = UnitOfWork.GetService<IProjectService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Project project = query.ToBo<Project>();
                Task task = project.ProjectTasks.SingleOrDefault(o => o.Id.Equals(childId));
                if (task != null)
                {
                    project.ProjectTasks.Remove(task);
                    var saveQuery = project.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(project.RetrieveData<ProjectData>());
                }
                else
                {
                    AddError(result.ValidationResult, "TaskCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "ProjectCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<ProjectData> SaveTask(TaskDto childDto, object parentId)
        {
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<ProjectData> result = new FacadeUpdateResult<ProjectData>();
            IProjectService projectService = UnitOfWork.GetService<IProjectService>();
            var projectQuery = projectService.Retrieve(parentId);
            if (projectQuery.HasResult)
            {
                Project project = projectQuery.ToBo<Project>();
                Task task = RetrieveOrNewTask(project, childDto.Id);
                if (task != null)
                {
                    task.Name = childDto.Name;
                    task.Description = childDto.Description;

                    var saveQuery = projectService.Save(project);
                    result.Merge(saveQuery);
                    result.AttachResult(project.RetrieveData<ProjectData>());
                }
                else
                {
                    AddError(result.ValidationResult, "TaskCannotBeFound");
                }
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IProjectService service = UnitOfWork.GetService<IProjectService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ProjectData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }

        internal TDto RetrieveTask<TDto>(object taskId, object projectId, IDataConverter<TaskData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("taskId", taskId);
            ArgumentValidator.IsNotNull("projectId", projectId);
            ArgumentValidator.IsNotNull("converter", converter);

            IProjectService service = UnitOfWork.GetService<IProjectService>();
            var query = service.Retrieve(projectId);
            if (query.HasResult)
            {
                Project project = query.ToBo<Project>();
                Task task = RetrieveOrNewTask(project, taskId);
                if (task != null)
                {
                    return converter.Convert(task.RetrieveData<TaskData>());
                }
            }

            return null;
        }

        internal TDto CreateNewTask<TDto>(object projectId, IDataConverter<TaskData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("projectId", projectId);
            ArgumentValidator.IsNotNull("converter", converter);

            IProjectService service = UnitOfWork.GetService<IProjectService>();
            var query = service.Retrieve(projectId);
            if (query.HasResult)
            {
                Project project = query.ToBo<Project>();
                Task task = RetrieveOrNewTask(project, null);
                if (task != null)
                {
                    return converter.Convert(task.RetrieveData<TaskData>());
                }
            }

            return null;
        }

        internal Task RetrieveOrNewTask(Project parent, object childId)
        {
            Task task = null;

            if (childId != null)
            {
                task = parent.ProjectTasks.SingleOrDefault(o => o.Id.Equals(childId));
            }
            else
            {
                task = parent.ProjectTasks.AddNewBo();
            }

            return task;
        }
    }
}
