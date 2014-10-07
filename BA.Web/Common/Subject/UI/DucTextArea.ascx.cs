using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucTextArea : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucTextArea.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtText, lblText);
            }

            if (SubjectField.MaxLength.HasValue)
            {
                txtText.MaxLength = SubjectField.MaxLength.Value;
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);
            string textValue = string.Empty;
            if (value != null)
            {
                textValue = value.ToString();
            }

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblText.Visible = true;
                    lblText.Text = textValue;
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    txtText.Text = textValue;
                    txtText.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtText.Text.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return txtText.Text.Trim();
        }
    }
}