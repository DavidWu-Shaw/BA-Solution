using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class DomainSetupMenuConverter : IDataConverter<DomainSetupMenuData, DomainSetupMenuDto>
    {
        public IEnumerable<DomainSetupMenuDto> Convert(IEnumerable<DomainSetupMenuData> entitys)
        {
            List<DomainSetupMenuDto> dtoList = new List<DomainSetupMenuDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public DomainSetupMenuDto Convert(DomainSetupMenuData entity)
        {
            DomainSetupMenuDto dto = new DomainSetupMenuDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.SetupMenuId.ToString();
            dto.SetupMenuId = entity.SetupMenuId;
            dto.Sort = entity.Sort;

            return dto;
        }
    }
}
