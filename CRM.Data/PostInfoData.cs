using System;
using Framework.Data;

namespace CRM.Data
{
    public class PostInfoData
    {
        public virtual object PostId { get; set; }
        public virtual object TopicId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual string IssuedBy { get; set; }
        public virtual DateTime IssuedTime { get; set; }
        public virtual byte[] Attachment { get; set; }
    }
}
