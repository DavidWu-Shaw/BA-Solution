using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class LanguageConverter : IDataConverter<LanguageData, LanguageDto>
    {
        public IEnumerable<LanguageDto> Convert(IEnumerable<LanguageData> entitys)
        {
            List<LanguageDto> dtoList = new List<LanguageDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public LanguageDto Convert(LanguageData entity)
        {
            LanguageDto dto = new LanguageDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Label = entity.Label;
            dto.Culture = entity.Culture;
            dto.IsPublished = entity.IsPublished;

            return dto;
        }
    }
}
