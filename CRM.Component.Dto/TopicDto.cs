using System;
using Framework.Core;
using System.Collections.Generic;

namespace CRM.Component.Dto
{
    public class TopicDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public object ForumId { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedTime { get; set; }
        public bool IsSticky { get; set; }
        public bool DenyReply { get; set; }
        public int NumberOfVisits { get; set; }

        public List<PostDto> Posts { get; set; }
    }
}
