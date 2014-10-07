using System;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucImage : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucImage.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, uploadImage, radImage);
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    radImage.Visible = true;
                    object value = base.LoadDucOriginalValue(subjectInstance);
                    if (value != null)
                    {
                        byte[] content = value as byte[];
                        radImage.DataValue = content;
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    uploadImage.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            bool isValid = uploadImage.InvalidFiles.Count == 0;
            if (!isValid)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = string.Format("Allowed file: *.jpg, *.jpeg, or file size exceeds 10M.");
            }
            return isValid;
        }

        public override object CollectDucValue()
        {
            if (uploadImage.UploadedFiles.Count > 0)
            {
                UploadedFile fileStream = uploadImage.UploadedFiles[0];

                byte[] fileData = new byte[fileStream.ContentLength];
                fileStream.InputStream.Read(fileData, 0, fileStream.ContentLength);

                return fileData;
            }

            return null;
        }
    }
}