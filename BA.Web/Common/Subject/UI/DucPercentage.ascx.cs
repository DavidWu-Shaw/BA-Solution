using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucPercentage : DucBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtPercentage, lblPercentage);
            }
            if (SubjectField.MaxLength.HasValue)
            {
                txtPercentage.MaxLength = SubjectField.MaxLength.Value;
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
                    lblPercentage.Visible = true;
                    if (numberValue.HasValue)
                    {
                        lblPercentage.Text = numberValue.ToString();
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (numberValue.HasValue)
                    {
                        txtPercentage.Text = numberValue.ToString();
                    }
                    txtPercentage.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtPercentage.Text.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return decimal.Parse(txtPercentage.Text.Trim());
        }
    }

}