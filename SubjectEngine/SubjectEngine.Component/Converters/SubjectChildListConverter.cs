using System.Collections.Generic;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectChildListConverter : IDataConverter<SubjectChildListData, SubjectChildListDto>
    {
        public IEnumerable<SubjectChildListDto> Convert(IEnumerable<SubjectChildListData> entitys)
        {
            List<SubjectChildListDto> dtoList = new List<SubjectChildListDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectChildListDto Convert(SubjectChildListData entity)
        {
            SubjectChildListDto dto = new SubjectChildListDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.ChildListSubjectId = entity.ChildListSubjectId;
            dto.Title = entity.Title;
            dto.AllowAdd = entity.AllowAdd;
            dto.AllowEdit = entity.AllowEdit;
            dto.AllowDelete = entity.AllowDelete;
            dto.Sort = entity.Sort;
            dto.AllowFiltering = entity.AllowFiltering;
            dto.AllowImport = entity.AllowImport;
            dto.ImportUrl = entity.ImportUrl;

            dto.SubjectChildListLanguages = new SubjectChildListLanguageConverter().Convert(entity.SubjectChildListLanguagesData);
            // Prepare dictionary
            dto.SubjectChildListLanguagesDic = new SortedDictionary<object, SubjectChildListLanguageDto>();
            foreach (SubjectChildListLanguageDto item in dto.SubjectChildListLanguages)
            {
                dto.SubjectChildListLanguagesDic.Add(item.LanguageId, item);
            }

            return dto;
        }
    }
}
