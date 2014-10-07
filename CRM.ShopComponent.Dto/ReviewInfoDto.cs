using System;

namespace CRM.ShopComponent.Dto
{
    public class ReviewInfoDto
    {
        public object ReviewId { get; set; }
        public object ObjectId { get; set; }
        public decimal? Rating { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public object IssuedById { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssuedTime { get; set; }
    }
}
