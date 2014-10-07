using System.Collections.Generic;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class DEntityItemConverter : IDataConverter<DEntityItemData, DEntityItemDto>
    {
        public IEnumerable<DEntityItemDto> Convert(IEnumerable<DEntityItemData> entitys)
        {
            List<DEntityItemDto> dtoList = new List<DEntityItemDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public DEntityItemDto Convert(DEntityItemData entity)
        {
            DEntityItemDto dto = new DEntityItemDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Value = entity.Value;
            dto.Text = entity.Text;
            return dto;
        }
    }
}
