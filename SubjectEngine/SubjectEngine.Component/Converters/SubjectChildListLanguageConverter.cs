using System.Collections.Generic;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectChildListLanguageConverter : IDataConverter<SubjectChildListLanguageData, SubjectChildListLanguageDto>
    {
        public IEnumerable<SubjectChildListLanguageDto> Convert(IEnumerable<SubjectChildListLanguageData> entitys)
        {
            List<SubjectChildListLanguageDto> dtoList = new List<SubjectChildListLanguageDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectChildListLanguageDto Convert(SubjectChildListLanguageData entity)
        {
            SubjectChildListLanguageDto dto = new SubjectChildListLanguageDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.LanguageId = entity.LanguageId;
            dto.Title = entity.Title;

            return dto;
        }
    }
}
