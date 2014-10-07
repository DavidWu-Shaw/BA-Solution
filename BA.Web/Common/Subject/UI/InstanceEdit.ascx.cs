using System;
using BA.Web.Common.Helper;
using BA.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class InstanceEdit : InstanceBaseControl
    {
        public event InstanceSavingEventHandler InstanceSaving;
        public event InstanceNewingEventHandler InstanceNewing;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CreateLayout(tblEdit, SubjectLoadMode.EditMode);

            if (!IsPostBack)
            {
                base.LoadInstanceData();
                SetButtonAttributes();
            }
        }

        private void SetButtonAttributes()
        {
            if (CurrentInstance.IsNew)
            {
                btnSaveAndAnother.Visible = true;
                btnCancel.PostBackUrl = GetUrl(CurrentSubject.ListUrl);
            }
            else
            {
                if (CurrentSubject.IsChildSubject)
                {
                    btnCancel.PostBackUrl = GetUrl(CurrentSubject.ManageUrl, CurrentInstance.Id) + string.Format("&{0}={1}", QryMasterInstanceId, MasterSubjectInstanceId);
                }
                else
                {
                    btnCancel.PostBackUrl = GetUrl(CurrentSubject.ManageUrl, CurrentInstance.Id);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            lblTitle.Text = GetTitle();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void btnSaveAndAnother_Click(object sender, EventArgs e)
        {
            bool isSuccessful = SaveData();
            if (isSuccessful)
            {
                // Get another new instance for editing
                InstanceNewingEventArgs arg = new InstanceNewingEventArgs(CurrentSubject.SubjectType);
                if (InstanceNewing != null)
                {
                    InstanceNewing(this, arg);
                }

                if (CurrentInstance != null)
                {
                    base.LoadInstanceData();
                }
            }
        }

        private bool SaveData()
        {
            bool isSuccessful = false;
            string display = string.Empty;

            bool isValid = ValidateValue();

            if (isValid)
            {
                SaveInstanceData();

                InstanceSavingEventArgs args = new InstanceSavingEventArgs(CurrentInstance);
                if (InstanceSaving != null)
                {
                    InstanceSaving(this, args);
                }

                if (args.IsSuccessful)
                {
                    //CurrentInstance = args.Instance;
                    isSuccessful = true;
                    display = Localize("Common.Subject.UI.InstanceEdit.SaveOK", "* Save OK.");
                }
                else
                {
                    isSuccessful = false;
                    display = Localize("Common.Subject.UI.InstanceEdit.SaveFailed", "* Save failed.");
                }
            }
            else
            {
                isSuccessful = false;
                display = Localize("Common.Subject.UI.InstanceEdit.InputInvalid", "* Input invalid.");
            }

            lbResultMsg.Text = display;
            return isSuccessful;
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblText.Text = Localize("Common.Subject.UI.InstanceEdit.lblText", "Edit");
            btnSave.Text = Localize("Common.Subject.UI.InstanceEdit.btnSave", "Save");
            btnSaveAndAnother.Text = Localize("Common.Subject.UI.InstanceEdit.btnSaveAndAnother", "Save & Another");
            btnCancel.Text = Localize("Common.Subject.UI.InstanceEdit.btnCancel", "Close");
        }
    }
}