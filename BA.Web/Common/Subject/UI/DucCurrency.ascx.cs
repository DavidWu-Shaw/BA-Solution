using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucCurrency : DucBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtCurrency, lblCurrency);
            }
            if (SubjectField.MaxLength.HasValue)
            {
                txtCurrency.MaxLength = SubjectField.MaxLength.Value;
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
                    lblCurrency.Visible = true;
                    if (numberValue.HasValue)
                    {
                        lblCurrency.Text = numberValue.ToString();
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (numberValue.HasValue)
                    {
                        txtCurrency.Text = numberValue.ToString();
                    }
                    txtCurrency.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtCurrency.Text.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return decimal.Parse(txtCurrency.Text.Trim());
        }
    }

}