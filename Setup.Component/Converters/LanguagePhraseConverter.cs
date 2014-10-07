using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class LanguagePhraseConverter : IDataConverter<LanguagePhraseData, LanguagePhraseDto>
    {
        public IEnumerable<LanguagePhraseDto> Convert(IEnumerable<LanguagePhraseData> entitys)
        {
            List<LanguagePhraseDto> dtoList = new List<LanguagePhraseDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public LanguagePhraseDto Convert(LanguagePhraseData entity)
        {
            LanguagePhraseDto dto = new LanguagePhraseDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.PhraseKey;
            dto.PhraseKey = entity.PhraseKey;
            dto.PhraseValue = entity.PhraseValue;
            dto.DateModified = entity.DateModified;

            return dto;
        }
    }
}
