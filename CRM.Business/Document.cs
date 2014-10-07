using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Document : BusinessObject<DocumentData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            IssuedDate = DateTime.Now;
        }

        [RequiredField("DocumentNameRequired", "The Name must be defined.")]
        [StringLength("DocumentNameLength", "The Name must have a length less than {1}", MaxLength = 100)]
        public string Name
        {
            get { return Data.Name; }
            set { Data.Name = value; }
        }

        [StringLength("DocumentDescriptionLength", "The Description must have a length less than {1}", MaxLength = 100)]
        public string Description
        {
            get { return Data.Description; }
            set { Data.Description = value; }
        }

        [StringLength("DocumentNotesLength", "The Notes must have a length less than {1}", MaxLength = 100)]
        public string Notes
        {
            get { return Data.Notes; }
            set { Data.Notes = value; }
        }

        [StringLength("DocumentFileNameLength", "The FileName must have a length less than {1}", MaxLength = 100)]
        public string FileName
        {
            get { return Data.FileName; }
            set { Data.FileName = value; }
        }

        public byte[] Content
        {
            get { return Data.Content; }
            set { Data.Content = value; }
        }

        public byte[] Thumbnail
        {
            get { return Data.Thumbnail; }
            set { Data.Thumbnail = value; }
        }

        public int DocTypeId
        {
            get { return Data.DocTypeId; }
            set { Data.DocTypeId = value; }
        }

        public object IssuedById
        {
            get { return Data.IssuedById; }
            set { Data.IssuedById = value; }
        }

        public DateTime IssuedDate
        {
            get { return Data.IssuedDate; }
            set { Data.IssuedDate = value; }
        }

        [StringLength("DocumentExtensionLength", "The Extension must have a length less than {1}", MaxLength = 10)]
        public string Extension
        {
            get
            {
                return Data.Extension;
            }
            set
            {
                Data.Extension = value;
            }
        }

        [StringLength("DocumentContentTypeLength", "The ContentType must have a length less than {1}", MaxLength = 100)]
        public string ContentType
        {
            get { return Data.ContentType; }
            set { Data.ContentType = value; }
        }

        public int ContentLength
        {
            get { return Data.ContentLength; }
            set { Data.ContentLength = value; }
        }

    }
}
