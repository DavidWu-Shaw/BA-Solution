using System;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucLookup : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucLookup.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, ddlLookup, hlLookup);
                ddlLookup.Height = WebContext.Current.ApplicationOption.DropDownHeight;
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            LoadDDL();

            object value = base.LoadDucOriginalValue(subjectInstance);
            if (value != null)
            {
                ddlLookup.SelectedValue = value.ToString();
            }

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    hlLookup.Visible = true;
                    if (ddlLookup.SelectedItem != null)
                    {
                        hlLookup.Text = ddlLookup.SelectedItem.Text;
                    }
                    if (SubjectField.LookupSubjectManageUrlFormatString.TrimHasValue())
                    {
                        hlLookup.NavigateUrl = GetUrl(SubjectField.LookupSubjectManageUrlFormatString, value);
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    ddlLookup.Enabled = !SubjectField.IsReadonly && !Disabled;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || ddlLookup.SelectedItem != null;
        }

        public override object CollectDucValue()
        {
            int result;
            if (int.TryParse(ddlLookup.SelectedValue, out result))
            {
                return result;
            }
            return null;
        }

        private void LoadDDL()
        {
            if (!SubjectField.IsRequired)
            {
                ddlLookup.Items.Add(new RadComboBoxItem(BindingListItem.EmptyText, BindingListItem.EmptyValue));
                ddlLookup.AppendDataBoundItems = true;
            }
            ddlLookup.DataSource = SubjectField.ListDataSource;
            ddlLookup.DataValueField = BindingListItem.ValueProperty;
            ddlLookup.DataTextField = BindingListItem.TextProperty;
            ddlLookup.DataBind();
        }
    }
}