using System;

namespace CRM.ShopComponent.Dto
{
    public class PostInfoDto
    {
        public object PostId { get; set; }
        public object TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public object IssuedById { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssuedTime { get; set; }
        public byte[] Attachment { get; set; }
    }
}
