using BA.Web.Common;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.Core;
using System.Collections.Generic;

namespace BA.Web.Converters
{
    public class CategoryConverter : IDataConverter<CategoryData, CategoryDto>
    {
        public object LanguageId { get; set; }

        public CategoryConverter()
        {
        }

        public CategoryConverter(object languageId)
        {
            LanguageId = languageId;
        }

        public IEnumerable<CategoryDto> Convert(IEnumerable<CategoryData> entitys)
        {
            List<CategoryDto> dtoList = new List<CategoryDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public CategoryDto Convert(CategoryData entity)
        {
            CategoryDto dto = new CategoryDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.ParentId = entity.ParentId;
            dto.CatalogId = entity.CatalogId;

            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && LanguageId != null)
            {
                dto.Name = WebContext.GetLocalizedData(CommonConst.CategoryKeyFormatString, entity.Id, LanguageId, entity.Name);
            }

            return dto;
        }
    }
}
