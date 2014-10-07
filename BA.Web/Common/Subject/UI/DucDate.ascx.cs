using BA.Core;
using Framework.Core;
using System;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucDate : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucDate.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, deDate, lblDate);
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblDate.Visible = true;
                    if (value != null)
                    {
                        DateTime dateValue = Convert.ToDateTime(value);
                        lblDate.Text = dateValue.ToString(UIConst.DateFormat);
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (value != null)
                    {
                        DateTime dateValue = Convert.ToDateTime(value);
                        deDate.SelectedDate = dateValue;
                    }
                    deDate.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || !deDate.SelectedDate.HasValue;
        }

        public override object CollectDucValue()
        {
            return deDate.SelectedDate;
        }
    }

}