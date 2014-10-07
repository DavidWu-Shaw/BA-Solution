using System.Collections.Generic;
using System.Linq;
using Framework.UoW;
using SubjectEngine.Business;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectConverter : IBusinessObjectConverter<Subject, SubjectDto>
    {
        public IEnumerable<SubjectDto> Convert(IEnumerable<Subject> entitys)
        {
            List<SubjectDto> dtoList = new List<SubjectDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectDto Convert(Subject entity)
        {
            SubjectDto dto = new SubjectDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.SubjectType = entity.SubjectType;
            dto.SubjectLabel = string.IsNullOrEmpty(entity.SubjectLabel) ? entity.SubjectType : entity.SubjectLabel;
            dto.SubjectIdField = entity.SubjectIdField;
            dto.MasterSubjectIdField = entity.MasterSubjectIdField;
            dto.ManageUrl = entity.ManageUrl;
            dto.EditUrl = entity.EditUrl;
            dto.ListUrl = entity.ListUrl;
            dto.ImportUrl = entity.ImportUrl;
            dto.AllowListImport = entity.AllowListImport;
            dto.AllowListFiltering = entity.AllowListFiltering;
            dto.AllowListAdd = entity.AllowListAdd;
            dto.AllowListEdit = entity.AllowListEdit;
            dto.AllowListDelete = entity.AllowListDelete;
            dto.IsAddInGrid = entity.IsAddInGrid;
            dto.IsGridInFormEdit = entity.IsGridInFormEdit;
            dto.IsChildSubject = entity.IsChildSubject;
            dto.RowIndexMax = entity.RowIndexMax;
            dto.ColIndexMax = entity.ColIndexMax;

            dto.SubjectFields = new SubjectFieldConverter().Convert(entity.SubjectFields.OrderBy(o => o.Sort));
            dto.SubjectChildLists = new SubjectChildListConverter().Convert(entity.RetrieveData<SubjectData>().SubjectChildListsData.OrderBy(o => o.Sort));

            dto.SubjectLanguages = new SubjectLanguageConverter().Convert(entity.RetrieveData<SubjectData>().SubjectLanguagesData);
            // Prepare dictionary
            dto.SubjectLanguagesDic = new SortedDictionary<object, SubjectLanguageDto>();
            foreach (SubjectLanguageDto item in dto.SubjectLanguages)
            {
                dto.SubjectLanguagesDic.Add(item.LanguageId, item);
            }

            return dto;
        }
    }
}
