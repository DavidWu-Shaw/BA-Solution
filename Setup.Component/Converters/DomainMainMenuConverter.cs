using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class DomainMainMenuConverter : IDataConverter<DomainMainMenuData, DomainMainMenuDto>
    {
        public IEnumerable<DomainMainMenuDto> Convert(IEnumerable<DomainMainMenuData> entitys)
        {
            List<DomainMainMenuDto> dtoList = new List<DomainMainMenuDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public DomainMainMenuDto Convert(DomainMainMenuData entity)
        {
            DomainMainMenuDto dto = new DomainMainMenuDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.MainMenuId.ToString();
            dto.MainMenuId = entity.MainMenuId;
            dto.Sort = entity.Sort;

            return dto;
        }
    }
}
