using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class CatalogConverter : IDataConverter<CatalogData, CatalogDto>
    {
        public IEnumerable<CatalogDto> Convert(IEnumerable<CatalogData> entitys)
        {
            List<CatalogDto> dtoList = new List<CatalogDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public CatalogDto Convert(CatalogData entity)
        {
            CatalogDto dto = new CatalogDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.IsDiscontinued = entity.IsDiscontinued;
            dto.IsGlobal = entity.IsGlobal;

            return dto;
        }
    }
}
