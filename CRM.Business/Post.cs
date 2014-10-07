using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Post : BusinessObject<PostData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            IssuedTime = DateTime.Now;
        }

        [RequiredField("PostContentRequired", "The Content must be defined.")]
        [StringLength("PostContentLength", "The Content must have a length less than {1}", MaxLength = 7999)]
        public string Content
        {
            get { return Data.Content; }
            set { Data.Content = value; }
        }

        [StringLength("PostTitleLength", "The Title must have a length less than {1}", MaxLength = 100)]
        public string Title
        {
            get { return Data.Title; }
            set { Data.Title = value; }
        }

        public object TopicId
        {
            get { return Data.TopicId; }
            set { Data.TopicId = value; }
        }

        public object IssuedById
        {
            get { return Data.IssuedById; }
            set { Data.IssuedById = value; }
        }

        public DateTime IssuedTime
        {
            get { return Data.IssuedTime; }
            set { Data.IssuedTime = value; }
        }

        public byte[] Attachment
        {
            get { return Data.Attachment; }
            set { Data.Attachment = value; }
        }
    }
}
