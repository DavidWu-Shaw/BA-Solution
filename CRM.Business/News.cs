using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class News : BusinessObject<NewsData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            IssuedTime = DateTime.Now;
        }

        [RequiredField("NewsTitleRequired", "The Title must be defined.")]
        [StringLength("NewsTitleLength", "The Title must have a length less than {1}", MaxLength = 100)]
        public string Title
        {
            get { return Data.Title; }
            set { Data.Title = value; }
        }

        [StringLength("NewsContentLength", "The Content must have a length less than {1}", MaxLength = 7999)]
        public string Content
        {
            get { return Data.Content; }
            set { Data.Content = value; }
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

        public int? NewsGroupId
        {
            get { return Data.NewsGroupId; }
            set { Data.NewsGroupId = value; }
        }

        public int? CategoryId
        {
            get { return Data.CategoryId; }
            set { Data.CategoryId = value; }
        }
    }
}
