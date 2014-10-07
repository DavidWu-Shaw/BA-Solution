using System.Collections.Generic;
using System.Linq;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class DEntityConverter : IDataConverter<DEntityData, DEntityDto>
    {
        public IEnumerable<DEntityDto> Convert(IEnumerable<DEntityData> entitys)
        {
            List<DEntityDto> dtoList = new List<DEntityDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public DEntityDto Convert(DEntityData entity)
        {
            DEntityDto dto = new DEntityDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Code = entity.Code;
            dto.Description = entity.Description;
            dto.EntityTypeId = entity.EntityTypeId;
            dto.IsBuiltIn = entity.IsBuiltIn;
            dto.AllowAddItem = !entity.IsBuiltIn && entity.AllowAddItem;
            dto.AllowEditItem = !entity.IsBuiltIn && entity.AllowEditItem;
            dto.AllowDeleteItem = !entity.IsBuiltIn && entity.AllowDeleteItem;

            dto.DEntityItems = new DEntityItemConverter().Convert(entity.DEntityItemsData.OrderBy(o => o.Value));
            return dto;
        }
    }
}
