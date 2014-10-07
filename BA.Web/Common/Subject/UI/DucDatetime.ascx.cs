using System;
using BA.Core;
using Framework.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucDatetime : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucDatetime.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, deDatatime, lblDatetime);
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            object value = base.LoadDucOriginalValue(subjectInstance);

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblDatetime.Visible = true;
                    if (value != null)
                    {
                        DateTime dateValue = Convert.ToDateTime(value);
                        lblDatetime.Text = dateValue.ToString(UIConst.DateTimeFormat);
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    if (value != null)
                    {
                        DateTime dateValue = Convert.ToDateTime(value);
                        deDatatime.SelectedDate = dateValue;
                    }
                    deDatatime.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || !deDatatime.SelectedDate.HasValue;
        }

        public override object CollectDucValue()
        {
            return deDatatime.SelectedDate;
        }
    }

}