using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class TopicBriefInfoConverter : IDataConverter<TopicBriefInfoData, TopicBriefInfoDto>
    {
        public IEnumerable<TopicBriefInfoDto> Convert(IEnumerable<TopicBriefInfoData> entitys)
        {
            List<TopicBriefInfoDto> dtoList = new List<TopicBriefInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public TopicBriefInfoDto Convert(TopicBriefInfoData entity)
        {
            TopicBriefInfoDto dto = new TopicBriefInfoDto();
            dto.TopicId = entity.TopicId;
            dto.Title = entity.Title;
            dto.IsSticky = entity.IsSticky;
            dto.DenyReply = entity.DenyReply;

            return dto;
        }
    }
}
