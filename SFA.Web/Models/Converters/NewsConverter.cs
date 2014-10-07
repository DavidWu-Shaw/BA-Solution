using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace SFA.Web.Models.Converters
{
    public class NewsConverter : IDataConverter<NewsData, NewsDto>
    {
        public IEnumerable<NewsDto> Convert(IEnumerable<NewsData> entitys)
        {
            List<NewsDto> dtoList = new List<NewsDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public NewsDto Convert(NewsData entity)
        {
            NewsDto dto = new NewsDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Title;
            dto.Title = entity.Title;
            dto.Content = entity.Content;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedTime = entity.IssuedTime;
            dto.NewsGroupId = entity.NewsGroupId;
            dto.CategoryId = entity.CategoryId;

            return dto;
        }
    }
}
