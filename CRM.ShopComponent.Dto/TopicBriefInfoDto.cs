using System.Collections.Generic;

namespace CRM.ShopComponent.Dto
{
    public class TopicBriefInfoDto
    {
        public object TopicId { get; set; }
        public string Title { get; set; }
        public bool IsSticky { get; set; }
        public bool DenyReply { get; set; }

        public List<PostInfoDto> Posts { get; set; }
    }
}
