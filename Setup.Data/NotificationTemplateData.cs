using System;
using Framework.Data;

namespace Setup.Data
{
    public class NotificationTemplateData : DataObject
    {
        public NotificationTemplateData()
        { }

        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string Culture { get; set; }
        public virtual int NotificationTypeId { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
    }
}
