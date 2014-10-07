using System.Collections.Generic;
using Framework.Component;
using Framework.UoW;
using PA.Component.Converters;
using PA.Component.Dto;
using PA.Data;

namespace PA.Component
{
    public class ProjectFacade : BusinessComponent
    {
        public ProjectFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ProjectSystem = new ProjectSystem(unitOfWork);
        }

        private ProjectSystem ProjectSystem { get; set; }

        public IEnumerable<ProjectDto> RetrieveAllProject()
        {
            return ProjectSystem.RetrieveAllProject(new ProjectConverter());
        }

        public ProjectDto RetrieveOrNewProject(object projectId)
        {
            return ProjectSystem.RetrieveOrNewProject(projectId, new ProjectConverter());
        }

        public IFacadeUpdateResult<ProjectData> SaveProject(ProjectDto projectDto)
        {
            return ProjectSystem.SaveProject(projectDto);
        }

        public IFacadeUpdateResult<ProjectData> DeleteProject(object projectId)
        {
            return ProjectSystem.DeleteProject(projectId);
        }

        public IFacadeUpdateResult<ProjectData> DeleteTask(object taskId, object projectId)
        {
            return ProjectSystem.DeleteTask(taskId, projectId);
        }

        public IFacadeUpdateResult<ProjectData> SaveTask(TaskDto taskDto, object projectId)
        {
            return ProjectSystem.SaveTask(taskDto, projectId);
        }

        public TaskDto RetrieveTask(object taskId, object projectId)
        {
            return ProjectSystem.RetrieveTask(taskId, projectId, new TaskConverter());
        }

        public TaskDto CreateNewTask(object projectId)
        {
            return ProjectSystem.CreateNewTask(projectId, new TaskConverter());
        }
    }
}
