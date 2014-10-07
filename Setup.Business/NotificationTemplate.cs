using System;
using Framework.Business;
using Framework.Validation;
using Setup.Data;

namespace Setup.Business
{
    public class NotificationTemplate : BusinessObject<NotificationTemplateData>
    {
        [RequiredField("SubjectRequired")]
        [StringLength("SubjectLength", MaxLength = 200)]
        public string Subject
        {
            get { return Data.Subject; }
            set { Data.Subject = value; }
        }


        [RequiredField("BodyRequired")]
        [StringLength("BodyLength", MinLength = 1)]
        public string Body
        {
            get { return Data.Body; }
            set { Data.Body = value; }
        }


        [RequiredField("CultureRequired")]
        [StringLength("CultureLength", MaxLength = 10)]
        [FormattedField("CulturePattern", @"[A-Z]{2}-[A-Z]{2}")]
        public string Culture
        {
            get { return Data.Culture; }
            set { Data.Culture = value; }
        }

        public int NotificationTypeId
        {
            get { return Data.NotificationTypeId; }
            set { Data.NotificationTypeId = value; }
        }

        public DateTime CreatedDate
        {
            get { return Data.CreatedDate; }
            set { Data.CreatedDate = value; }
        }
        public DateTime ModifiedDate
        {
            get { return Data.ModifiedDate; }
            set { Data.ModifiedDate = value; }
        }
    }
}
