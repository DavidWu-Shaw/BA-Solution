using System;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class PostDto : BaseDto
    {
        public object TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedTime { get; set; }
        public byte[] Attachment { get; set; }
    }
}
