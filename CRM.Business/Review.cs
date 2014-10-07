using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Review : BusinessObject<ReviewData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            IssuedTime = DateTime.Now;
        }

        public object ObjectId
        {
            get { return Data.ObjectId; }
            set { Data.ObjectId = value; }
        }

        public int ObjectTypeId
        {
            get { return Data.ObjectTypeId; }
            set { Data.ObjectTypeId = value; }
        }

        public decimal? Rating
        {
            get { return Data.Rating; }
            set { Data.Rating = value; }
        }

        [RequiredField("ReviewContentRequired", "The Content must be defined.")]
        [StringLength("ReviewContentLength", "The Content must have a length less than {1}", MaxLength = 7999)]
        public string Content
        {
            get { return Data.Content; }
            set { Data.Content = value; }
        }

        [StringLength("ReviewTitleLength", "The Title must have a length less than {1}", MaxLength = 100)]
        public string Title
        {
            get { return Data.Title; }
            set { Data.Title = value; }
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
    }
}
