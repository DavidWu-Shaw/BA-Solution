using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Topic : BusinessObject<TopicData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            IssuedTime = DateTime.Now;
            IsSticky = false;
            DenyReply = false;
        }

        [RequiredField("TopicTitleRequired", "The Title must be defined.")]
        [StringLength("TopicTitleLength", "The Title must have a length less than {1}", MaxLength = 200)]
        public string Title
        {
            get { return Data.Title; }
            set { Data.Title = value; }
        }

        public object ForumId
        {
            get { return Data.ForumId; }
            set { Data.ForumId = value; }
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

        public bool IsSticky
        {
            get { return Data.IsSticky; }
            set { Data.IsSticky = value; }
        }

        public bool DenyReply
        {
            get { return Data.DenyReply; }
            set { Data.DenyReply = value; }
        }

        public int NumberOfVisits
        {
            get { return Data.NumberOfVisits; }
            set { Data.NumberOfVisits = value; }
        }
    }
}
