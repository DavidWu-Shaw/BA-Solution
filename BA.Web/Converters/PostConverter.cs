using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class PostConverter : IDataConverter<PostData, PostDto>
    {
        public IEnumerable<PostDto> Convert(IEnumerable<PostData> entitys)
        {
            List<PostDto> dtoList = new List<PostDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public PostDto Convert(PostData entity)
        {
            PostDto dto = new PostDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Title;
            dto.Title = entity.Title;
            dto.Content = entity.Content;
            dto.TopicId = entity.TopicId;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedTime = entity.IssuedTime;
            dto.Attachment = entity.Attachment;

            return dto;
        }
    }
}
