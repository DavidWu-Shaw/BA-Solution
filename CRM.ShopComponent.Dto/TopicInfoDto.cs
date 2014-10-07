using System;

namespace CRM.ShopComponent.Dto
{
    public class TopicInfoDto
    {
        public object TopicId { get; set; }
        public string Title { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedTime { get; set; }
        public bool IsSticky { get; set; }
        public bool DenyReply { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfVisits { get; set; }
        public string IssuedBy { get; set; }
        public string LastPostBy { get; set; }
        public DateTime LastPostTime { get; set; }
    }
}
