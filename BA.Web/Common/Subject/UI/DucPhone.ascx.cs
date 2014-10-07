using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucPhone : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucPhone.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtPhone, lblPhone);
            }

            if (SubjectField.MaxLength.HasValue)
            {
                txtPhone.MaxLength = SubjectField.MaxLength.Value;
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
                    lblPhone.Visible = true;
                    lblPhone.Text = textValue;
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    txtPhone.Text = textValue;
                    txtPhone.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtPhone.Text != string.Empty;
        }

        public override object CollectDucValue()
        {
            return txtPhone.Text.Trim();
        }
    }

}