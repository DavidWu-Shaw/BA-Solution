using System.Collections.Generic;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectFieldLanguageConverter : IDataConverter<SubjectFieldLanguageData, SubjectFieldLanguageDto>
    {
        public IEnumerable<SubjectFieldLanguageDto> Convert(IEnumerable<SubjectFieldLanguageData> entitys)
        {
            List<SubjectFieldLanguageDto> dtoList = new List<SubjectFieldLanguageDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectFieldLanguageDto Convert(SubjectFieldLanguageData entity)
        {
            SubjectFieldLanguageDto dto = new SubjectFieldLanguageDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.LanguageId = entity.LanguageId;
            dto.FieldLabel = entity.FieldLabel;
            dto.HelpText = entity.HelpText;

            return dto;
        }
    }
}
