using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class DataPhraseConverter : IDataConverter<DataPhraseData, DataPhraseDto>
    {
        public IEnumerable<DataPhraseDto> Convert(IEnumerable<DataPhraseData> entitys)
        {
            List<DataPhraseDto> dtoList = new List<DataPhraseDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public DataPhraseDto Convert(DataPhraseData entity)
        {
            DataPhraseDto dto = new DataPhraseDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.PhraseKey;
            dto.PhraseKey = entity.PhraseKey;
            dto.PhraseValue = entity.PhraseValue;

            return dto;
        }
    }
}
