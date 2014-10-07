using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class ApplicationSettingConverter : IDataConverter<ApplicationSettingData, ApplicationSettingDto>
    {
        public IEnumerable<ApplicationSettingDto> Convert(IEnumerable<ApplicationSettingData> entitys)
        {
            List<ApplicationSettingDto> dtoList = new List<ApplicationSettingDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ApplicationSettingDto Convert(ApplicationSettingData entity)
        {
            ApplicationSettingDto dto = new ApplicationSettingDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.SettingKey;
            dto.SettingKey = entity.SettingKey;
            dto.SettingValue = entity.SettingValue;

            return dto;
        }
    }
}
