using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class DomainConverter : IDataConverter<DomainData, DomainDto>
    {
        public IEnumerable<DomainDto> Convert(IEnumerable<DomainData> entitys)
        {
            List<DomainDto> dtoList = new List<DomainDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public DomainDto Convert(DomainData entity)
        {
            DomainDto dto = new DomainDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.RelatedSubjectIdField = entity.RelatedSubjectIdField;
            dto.DefaultUrl = entity.DefaultUrl;

            dto.DomainMainMenus = new DomainMainMenuConverter().Convert(entity.DomainMainMenusData.OrderBy(o => o.Sort));
            dto.DomainSetupMenus = new DomainSetupMenuConverter().Convert(entity.DomainSetupMenusData.OrderBy(o => o.Sort));

            return dto;
        }
    }
}
