using System;
using Framework.Data;

namespace CRM.Data
{
    public class ReviewData : DataObject
    {
        public virtual object ObjectId { get; set; }
        public virtual int ObjectTypeId { get; set; }
        public virtual decimal? Rating { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual DateTime IssuedTime { get; set; }
    }
}
