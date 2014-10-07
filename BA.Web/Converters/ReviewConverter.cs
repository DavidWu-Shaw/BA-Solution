using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class ReviewConverter : IDataConverter<ReviewData, ReviewDto>
    {
        public IEnumerable<ReviewDto> Convert(IEnumerable<ReviewData> entitys)
        {
            List<ReviewDto> dtoList = new List<ReviewDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ReviewDto Convert(ReviewData entity)
        {
            ReviewDto dto = new ReviewDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Id.ToString();
            dto.ObjectId = entity.ObjectId;
            dto.ObjectTypeId = entity.ObjectTypeId;
            dto.Rating = entity.Rating;
            dto.Title = entity.Title;
            dto.Content = entity.Content;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedTime = entity.IssuedTime;

            return dto;
        }
    }
}
