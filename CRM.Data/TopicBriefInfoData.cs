using System;
using Framework.Data;

namespace CRM.Data
{
    public class TopicBriefInfoData
    {
        public virtual object TopicId { get; set; }
        public virtual string Title { get; set; }
        public virtual int? ForumId { get; set; }
        public virtual bool IsSticky { get; set; }
        public virtual bool DenyReply { get; set; }
    }
}
