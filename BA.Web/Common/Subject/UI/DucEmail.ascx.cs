using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucEmail : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucEmail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtEmail, lblEmail);
            }

            if (SubjectField.MaxLength.HasValue)
            {
                txtEmail.MaxLength = SubjectField.MaxLength.Value;
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
                    lblEmail.Visible = true;
                    lblEmail.Text = textValue;
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    txtEmail.Text = textValue;
                    txtEmail.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtEmail.Text.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return txtEmail.Text.Trim();
        }
    }
}