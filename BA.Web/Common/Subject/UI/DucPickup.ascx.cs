using System;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Subject.UI
{
    public partial class DucPickup : DucBaseControl
    {
        public const string ControlURL = @"/Common/Subject/UI/DucPickup.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlAttribute(lblLabel, ddlPickup, lblPickup);
                ddlPickup.Height = WebContext.Current.ApplicationOption.DropDownHeight;
            }
        }

        public override void LoadDucValue(BaseDto subjectInstance)
        {
            LoadDDL();

            object value = base.LoadDucOriginalValue(subjectInstance);
            if (value != null && value is int)
            {
                ddlPickup.SelectedValue = value.ToString();
            }

            switch (LoadMode)
            {
                case SubjectLoadMode.DetailMode:
                    lblPickup.Visible = true;
                    if (ddlPickup.SelectedItem != null)
                    {
                        lblPickup.Text = ddlPickup.SelectedItem.Text;
                    }
                    break;
                case SubjectLoadMode.EditMode:
                    tdInput.Visible = true;
                    ddlPickup.Enabled = !SubjectField.IsReadonly;
                    lblMark.Visible = SubjectField.IsRequired;
                    break;
                default:
                    break;
            }
        }

        public override bool ValidateValue()
        {
            return !SubjectField.IsRequired || ddlPickup.SelectedItem != null;
        }

        public override object CollectDucValue()
        {
            int result;
            if (int.TryParse(ddlPickup.SelectedValue, out result))
            {
                return result;
            }
            return null;
        }

        private void LoadDDL()
        {
            if (!SubjectField.IsRequired)
            {
                ddlPickup.Items.Add(new RadComboBoxItem(BindingListItem.EmptyText, BindingListItem.EmptyValue));
                ddlPickup.AppendDataBoundItems = true;
            }
            ddlPickup.DataSource = SubjectField.ListDataSource;
            ddlPickup.DataValueField = BindingListItem.ValueProperty;
            ddlPickup.DataTextField = BindingListItem.TextProperty;
            ddlPickup.DataBind();
        }
    }
}