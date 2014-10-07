using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class PostInfoConverter : IDataConverter<PostInfoData, PostInfoDto>
    {
        public IEnumerable<PostInfoDto> Convert(IEnumerable<PostInfoData> entitys)
        {
            List<PostInfoDto> dtoList = new List<PostInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public PostInfoDto Convert(PostInfoData entity)
        {
            PostInfoDto dto = new PostInfoDto();
            dto.PostId = entity.PostId;
            dto.TopicId = entity.TopicId;
            dto.Content = entity.Content;
            dto.Title = entity.Title;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedBy = entity.IssuedBy;
            dto.IssuedTime = entity.IssuedTime;
            dto.Attachment = entity.Attachment;

            return dto;
        }
    }
}
