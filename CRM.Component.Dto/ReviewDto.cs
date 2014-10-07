using System;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class ReviewDto : BaseDto
    {
        public object ObjectId { get; set; }
        public int ObjectTypeId { get; set; }
        public decimal? Rating { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedTime { get; set; }
    }
}
