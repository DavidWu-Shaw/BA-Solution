using System;
using Framework.Data;

namespace CRM.Data
{
    public class ReviewInfoData
    {
        public virtual object ReviewId { get; set; }
        public virtual decimal? Rating { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual string IssuedBy { get; set; }
        public virtual DateTime IssuedTime { get; set; }
    }
}
