using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class ReviewInfoConverter : IDataConverter<ReviewInfoData, ReviewInfoDto>
    {
        public IEnumerable<ReviewInfoDto> Convert(IEnumerable<ReviewInfoData> entitys)
        {
            List<ReviewInfoDto> dtoList = new List<ReviewInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ReviewInfoDto Convert(ReviewInfoData entity)
        {
            ReviewInfoDto dto = new ReviewInfoDto();
            dto.ReviewId = entity.ReviewId;
            dto.Rating = entity.Rating;
            dto.Title = entity.Title;
            dto.Content = entity.Content;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedBy = entity.IssuedBy;
            dto.IssuedTime = entity.IssuedTime;

            return dto;
        }
    }
}
