using System.Web.UI.WebControls;
using Framework.Core;
using Framework.Core.Helpers;
using SubjectEngine.Component.Dto;

namespace BA.Web.Common.Subject.UI
{
    public enum SubjectLoadMode
    {
        DetailMode = 1,
        EditMode = 2,
    }

    public class DucBaseControl : BaseControl
    {
        public const string ControlURLFormatString = "/Common/Subject/UI/Duc{0}.ascx";

        protected const int LabelWith = 180;
        protected const int ValueWidth = 240;

        public SubjectFieldDto SubjectField { get; set; }
        public SubjectLoadMode LoadMode { get; set; }
        public bool Disabled { get; set; }

        protected virtual void SetControlAttribute(Label label, WebControl editableControl, WebControl readonlyControl)
        {
            label.Width = LabelWith;
            label.Text = GetLabelText();

            editableControl.Width = ValueWidth;
            editableControl.ToolTip = SubjectField.HelpText;

            readonlyControl.Width = ValueWidth;
        }

        public string GetLabelText()
        {
            if (SubjectField.FieldLabel != null && SubjectField.FieldLabel.Length > 0)
            {
                // Todo: implement multi-language
                return SubjectField.GetFieldLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            }
            else
            {
                return SubjectField.FieldKey;
            }
        }

        protected object LoadDucOriginalValue(BaseDto subjectInstance)
        {
            return ReflectionHelper.GetValue(subjectInstance, SubjectField.FieldKey);
        }

        public virtual void LoadDucValue(BaseDto subjectInstance)
        {
        }

        public virtual object CollectDucValue()
        {
            return null;
        }

        public virtual bool ValidateValue()
        {
            return true;
        }

        public virtual void SaveDucValue(BaseDto subjectInstance)
        {
            ReflectionHelper.SetValue(subjectInstance, SubjectField.FieldKey, CollectDucValue());
        }
    }
}
