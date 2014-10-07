using System;
using BA.Core;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucTime : DucBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, deTime, lblTime);
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblTime.Visible = true;
                    if (value != null)
                    {
                        DateTime dateValue = Convert.ToDateTime(value);
                        lblTime.Text = dateValue.ToString(UIConst.TimeFormat);
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (value != null)
                    {
                        DateTime dateValue = Convert.ToDateTime(value);
                        deTime.SelectedDate = dateValue;
                    }
                    deTime.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || !deTime.SelectedDate.HasValue;
        }

        public override object CollectDucValue()
        {
            return deTime.SelectedDate;
        }
    }

}