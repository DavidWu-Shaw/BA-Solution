using System;
using Framework.Data;

namespace CRM.Data
{
    public class TopicData : DataObject
    {
        public virtual string Title { get; set; }
        public virtual object ForumId { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual DateTime IssuedTime { get; set; }
        public virtual bool IsSticky { get; set; }
        public virtual bool DenyReply { get; set; }
        public virtual int NumberOfVisits { get; set; }
    }
}
