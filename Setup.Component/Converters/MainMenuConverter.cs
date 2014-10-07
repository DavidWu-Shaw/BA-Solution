using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
{
    public class MainMenuConverter : IDataConverter<MainMenuData, MainMenuDto>
    {
        public IEnumerable<MainMenuDto> Convert(IEnumerable<MainMenuData> entitys)
        {
            List<MainMenuDto> dtoList = new List<MainMenuDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public MainMenuDto Convert(MainMenuData entity)
        {
            MainMenuDto dto = new MainMenuDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.MenuText = entity.MenuText;
            dto.Tooltip = entity.Tooltip;
            dto.NavigateUrl = entity.NavigateUrl;
            dto.ImageUrl = entity.ImageUrl;
            dto.Sort = entity.Sort;

            return dto;
        }
    }
}
