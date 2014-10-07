using System;
using Framework.Data;

namespace CRM.Data
{
    public class NewsData : DataObject
    {
        public NewsData()
        {
        }

        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual DateTime IssuedTime { get; set; }
        public virtual int? NewsGroupId { get; set; }
        public virtual int? CategoryId { get; set; }
    }
}
