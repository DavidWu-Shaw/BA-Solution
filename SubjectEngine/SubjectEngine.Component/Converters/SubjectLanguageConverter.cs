using System.Collections.Generic;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectLanguageConverter : IDataConverter<SubjectLanguageData, SubjectLanguageDto>
    {
        public IEnumerable<SubjectLanguageDto> Convert(IEnumerable<SubjectLanguageData> entitys)
        {
            List<SubjectLanguageDto> dtoList = new List<SubjectLanguageDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectLanguageDto Convert(SubjectLanguageData entity)
        {
            SubjectLanguageDto dto = new SubjectLanguageDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.LanguageId = entity.LanguageId;
            dto.SubjectLabel = entity.SubjectLabel;

            return dto;
        }
    }
}
