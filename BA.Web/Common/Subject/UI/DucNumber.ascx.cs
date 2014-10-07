using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucNumber : DucBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtNumber, lblNumber);
            }
            if (SubjectField.MaxLength.HasValue)
            {
                txtNumber.MaxLength = SubjectField.MaxLength.Value;
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);
            decimal? numberValue = null;
            if (value != null)
            {
                numberValue = Convert.ToDecimal(value);
            }

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblNumber.Visible = true;
                    if (numberValue.HasValue)
                    {
                        lblNumber.Text = numberValue.ToString();
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (numberValue.HasValue)
                    {
                        txtNumber.Text = numberValue.ToString();
                    }
                    txtNumber.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtNumber.Text.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return decimal.Parse(txtNumber.Text.Trim());
        }
    }

}