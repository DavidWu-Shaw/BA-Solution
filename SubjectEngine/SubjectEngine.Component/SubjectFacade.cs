using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using SubjectEngine.Component.Converters;
using SubjectEngine.Component.Dto;
using SubjectEngine.Core;
using SubjectEngine.Data;

namespace SubjectEngine.Component
{
    public class SubjectFacade : BusinessComponent
    {
        public SubjectFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            SubjectSystem = new SubjectSystem(unitOfWork);
        }

        private SubjectSystem SubjectSystem { get; set; }

        public IEnumerable<SubjectDto> RetrieveAllSubject()
        {
            return SubjectSystem.RetrieveAllSubject(new SubjectConverter());
        }

        public SubjectDto RetrieveSubject(object subjectId)
        {
            SubjectDto subjectDto = SubjectSystem.RetrieveSubject<SubjectDto>(subjectId, new SubjectConverter());
            return subjectDto;
        }

        public SubjectDto RetrieveSubjectByType(string subjectType)
        {
            SubjectDto subjectDto = SubjectSystem.RetrieveSubjectByType<SubjectDto>(subjectType, new SubjectConverter());
            // Prepare Id ordered subject list for looking up
            Dictionary<object, SubjectDto> subjectDic = new Dictionary<object, SubjectDto>();
            foreach (SubjectDto subject in SubjectSystem.RetrieveAllSubject(new SubjectConverter()))
            {
                subjectDic.Add(subject.Id, subject);
            }
            // Attach SubjectField datasource and LookupSubjectManageUrlFormatString to fields
            AttachProperties(subjectDto, subjectDic);
            return subjectDto;
        }

        public IEnumerable<SubjectFieldInfoDto> RetrieveSubjectFieldInfos(object subjectId)
        {
            return SubjectSystem.RetrieveSubjectFieldInfos(subjectId, new SubjectFieldInfoConverter());
        }

        public IFacadeUpdateResult<SubjectData> SaveSubjectField(object subjectId, SubjectFieldInfoDto info)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SubjectData> result = SubjectSystem.SaveSubjectField(subjectId, info);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<SubjectData> DeleteSubjectField(object subjectId, object fieldId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SubjectData> result = SubjectSystem.DeleteSubjectField(subjectId, fieldId);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<SubjectData> SaveSubjectChildList(object parentId, SubjectChildListDto childDto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SubjectData> result = SubjectSystem.SaveSubjectChildList(parentId, childDto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<SubjectData> DeleteSubjectChildList(object parentId, object childId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SubjectData> result = SubjectSystem.DeleteSubjectChildList(parentId, childId);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IList<BindingListItem> GetBindingList()
        {
            return SubjectSystem.GetBindingList();
        }

        public Dictionary<string, SubjectDto> GetSubjectList()
        {
            Dictionary<string, SubjectDto> subjectList = new Dictionary<string, SubjectDto>();

            IEnumerable<SubjectDto> subjects = SubjectSystem.RetrieveAllSubject(new SubjectConverter());

            // Prepare Id ordered subject list for looking up
            Dictionary<object, SubjectDto> subjectDic = new Dictionary<object, SubjectDto>();            
            foreach (SubjectDto subject in subjects)
            {
                subjectDic.Add(subject.Id, subject);
            }

            foreach (SubjectDto subject in subjects)
            {
                subjectList.Add(subject.SubjectType, subject);
                AttachProperties(subject, subjectDic);
            }

            return subjectList;
        }

        // Attach properties to SubjectFields
        private void AttachProperties(SubjectDto subjectDto, Dictionary<object, SubjectDto> subjectDic)
        {
            // 1. Attach SubjectField properties
            foreach (SubjectFieldDto field in subjectDto.SubjectFields)
            {
                if (field.DucType == DucTypes.Lookup && field.LookupSubjectId != null)
                {
                    field.LookupSubjectManageUrlFormatString = subjectDic[field.LookupSubjectId].ManageUrl;
                    field.LookupSubjectType = subjectDic[field.LookupSubjectId].SubjectType;
                    // Link to another Subject, will be retrieved in app module based on LookupSubjectType
                }

                // Retrieve ManaeUrl
                if (field.IsLinkInGrid)
                {
                    if (field.DucType == DucTypes.Lookup && field.LookupSubjectId != null)
                    {
                        // Link to another Subject, will be retrieved in app module
                    }
                    else
                    {
                        // Link to current subject
                        field.LookupSubjectManageUrlFormatString = subjectDto.ManageUrl;
                    }
                }
            }
            // 2. Attach SubjectChildList properties
            foreach (SubjectChildListDto childList in subjectDto.SubjectChildLists)
            {
                childList.ChildListSubject = subjectDic[childList.ChildListSubjectId];
            }
        }
    }
}
