using System;
using Framework.Data;

namespace CRM.Data
{
    public class DocumentData : DataObject
    {
        public DocumentData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Notes { get; set; }
        public virtual string FileName { get; set; }
        public virtual byte[] Content { get; set; }
        public virtual byte[] Thumbnail { get; set; }
        public virtual int DocTypeId { get; set; }
        public virtual object IssuedById { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual string Extension { get; set; }
        public virtual string ContentType { get; set; }
        public virtual int ContentLength { get; set; }
    }
}
