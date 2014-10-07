using System;
using System.Web.UI.WebControls;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucMemo : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucMemo.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, txtText, lblText);
            }

            if (SubjectField.MaxLength.HasValue)
            {
                txtText.MaxTextLength= SubjectField.MaxLength.Value;
            }
        }

        protected override void SetControlAttribute(Label label, WebControl editableControl, WebControl readonlyControl)
        {
            base.SetControlAttribute(label, editableControl, readonlyControl);

            editableControl.Width = 300;

            txtText.ToolsFile = ServerPath + "/Styles/BasicTools.xml";
            txtText.EditModes = Telerik.Web.UI.EditModes.Design;
            EditorModule moduleStatistics = new EditorModule();
            moduleStatistics.Name = "RadEditorStatistics";
            txtText.Modules.Add(moduleStatistics);
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
                    lblText.Visible = true;
                    lblText.Text = textValue;
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    txtText.Content = textValue;
                    txtText.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || txtText.Content.Trim() != string.Empty;
        }

        public override object CollectDucValue()
        {
            return txtText.Content.Trim();
        }
    }
}