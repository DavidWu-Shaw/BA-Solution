using System.Collections.Generic;
using Framework.Component;
using Framework.UoW;
using PA.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Core;
using Framework.Core;

namespace PA.Component
{
    public class PASubjectFacade : BusinessComponent
    {
        public PASubjectFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void AttachProperties(SubjectDto subjectDto)
        {
            foreach (SubjectFieldDto field in subjectDto.SubjectFields)
            {
                // Retrieve Lookup type ListDataSource
                if (field.DucType == DucTypes.Lookup && field.LookupSubjectId != null)
                {
                    field.ListDataSource = GetBindingList(field.LookupSubjectType);
                }
            }
        }

        private IList<BindingListItem> GetBindingList(string subjectType)
        {
            IList<BindingListItem> dataSource = null;

            switch (subjectType)
            {
                case SubjectTypeRegistry.User:
                    UserSystem userSys = new UserSystem(UnitOfWork);
                    dataSource = userSys.GetBindingList();
                    break;
                case SubjectTypeRegistry.Project:
                    ProjectSystem projSys = new ProjectSystem(UnitOfWork);
                    dataSource = projSys.GetBindingList();
                    break;

            }

            return dataSource;
        }
    }
}
