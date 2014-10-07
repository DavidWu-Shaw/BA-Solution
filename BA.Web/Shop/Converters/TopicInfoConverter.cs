using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class TopicInfoConverter : IDataConverter<TopicInfoData, TopicInfoDto>
    {
        public IEnumerable<TopicInfoDto> Convert(IEnumerable<TopicInfoData> entitys)
        {
            List<TopicInfoDto> dtoList = new List<TopicInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public TopicInfoDto Convert(TopicInfoData entity)
        {
            TopicInfoDto dto = new TopicInfoDto();
            dto.TopicId = entity.TopicId;
            dto.Title = entity.Title;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedBy = entity.IssuedBy;
            dto.IsSticky = entity.IsSticky;
            dto.DenyReply = entity.DenyReply;
            dto.IssuedTime = entity.IssuedTime;
            dto.NumberOfPosts = entity.NumberOfPosts;
            dto.NumberOfVisits = entity.NumberOfVisits;
            dto.LastPostBy = entity.LastPostBy;
            dto.LastPostTime = entity.LastPostTime;

            return dto;
        }
    }
}
