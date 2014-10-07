using System;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucCheckbox : DucBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, chkBool, lblBool);
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);
            chkBool.Checked = Convert.ToBoolean(value);

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    chkBool.Enabled = false;
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    chkBool.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return true;
        }

        public override object CollectDucValue()
        {
            return chkBool.Checked;
        }
    }
}