using System;
using Framework.Data;

namespace CRM.Data
{
    public class TopicInfoData
    {
        public virtual object TopicId { get; set; }
        public virtual string Title { get; set; }
        public virtual int? ForumId { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual DateTime IssuedTime { get; set; }
        public virtual bool IsSticky { get; set; }
        public virtual bool DenyReply { get; set; }
        public virtual int NumberOfVisits { get; set; }

        public virtual int NumberOfPosts { get; set; }
        public virtual string IssuedBy { get; set; }
        public virtual string LastPostBy { get; set; }
        public virtual DateTime LastPostTime { get; set; }
    }
}
