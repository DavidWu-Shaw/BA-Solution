using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucInteger : DucBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtInteger, lblInteger);
            }
            if (SubjectField.MaxLength.HasValue)
            {
                txtInteger.MaxLength = SubjectField.MaxLength.Value;
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);
            int? numberValue = null;
            if (value != null)
            {
                numberValue = Convert.ToInt32(value);
            }

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblInteger.Visible = true;
                    if (numberValue.HasValue)
                    {
                        lblInteger.Text = numberValue.ToString();
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (numberValue.HasValue)
                    {
                        txtInteger.Text = numberValue.ToString();
                    }
                    txtInteger.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtInteger.Text.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return int.Parse(txtInteger.Text.Trim());
        }
    }

}