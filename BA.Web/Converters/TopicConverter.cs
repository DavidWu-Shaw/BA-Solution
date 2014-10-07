using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class TopicConverter : IDataConverter<TopicData, TopicDto>
    {
        public IEnumerable<TopicDto> Convert(IEnumerable<TopicData> entitys)
        {
            List<TopicDto> dtoList = new List<TopicDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public TopicDto Convert(TopicData entity)
        {
            TopicDto dto = new TopicDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Title;
            dto.Title = entity.Title;
            dto.ForumId = entity.ForumId;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedTime = entity.IssuedTime;
            dto.IsSticky = entity.IsSticky;
            dto.DenyReply = entity.DenyReply;
            dto.NumberOfVisits = entity.NumberOfVisits;

            return dto;
        }
    }
}
