using System;
using BA.Core;
using Framework.Core;
using Framework.Core.Helpers;
using Telerik.Web.UI;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucAttachment : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucAttachment.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, uploadAttachment, hlAttachment);
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    hlAttachment.Visible = true;
                    object value = ReflectionHelper.GetValue(subjectInstance, UIConst.FLD_LinkText);
                    if (value != null)
                    {
                        hlAttachment.Text = value.ToString();
                    }
                    if (!string.IsNullOrEmpty(SubjectField.NavigateUrlFormatString))
                    {
                        hlAttachment.NavigateUrl = ServerPath + string.Format(SubjectField.NavigateUrlFormatString, subjectInstance.Id);
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    uploadAttachment.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return uploadAttachment.InvalidFiles.Count == 0;
        }

        public override object CollectDucValue()
        {
            if (uploadAttachment.UploadedFiles.Count > 0)
            {
                UploadedFile fileStream = uploadAttachment.UploadedFiles[0];

                byte[] fileData = new byte[fileStream.ContentLength];
                fileStream.InputStream.Read(fileData, 0, fileStream.ContentLength);

                return fileData;
            }

            return null;
        }
    }
}