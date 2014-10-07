using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class SetupMenuConverter : IDataConverter<SetupMenuData, SetupMenuDto>
    {
        public IEnumerable<SetupMenuDto> Convert(IEnumerable<SetupMenuData> entitys)
        {
            List<SetupMenuDto> dtoList = new List<SetupMenuDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public SetupMenuDto Convert(SetupMenuData entity)
        {
            SetupMenuDto dto = new SetupMenuDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.ParentMenuId = entity.ParentMenuId;
            dto.MenuText = entity.MenuText;
            dto.Tooltip = entity.Tooltip;
            dto.NavigateUrl = entity.NavigateUrl;
            dto.Sort = entity.Sort;
            dto.MenuTypeId = entity.MenuTypeId;

            return dto;
        }
    }
}
