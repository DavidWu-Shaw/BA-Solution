using System;
using SubjectEngine.Component.Dto;
using System.Web.UI.HtmlControls;
using BA.Web.Common.Helper;

namespace BA.Web.Common.Subject.UI
{
    public partial class InstanceDetail : InstanceBaseControl
    {
        public const string ControlURL = "/Common/Subject/UI/InstanceDetail.ascx";


        public event NeedListInstancesEventHandler ChildListNeedInstances;
        public event InstanceRowNewingEventHandler ChildListInstanceRowNewing;
        public event InstanceRowSavingEventHandler ChildListInstanceRowSaving;
        public event InstanceRowDeletingEventHandler ChildListInstanceRowDeleting;

        public string ControllingFieldName { get; set; }

        public bool AllowModify { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ControllingFieldName = CurrentSubject.SubjectIdField;

            base.CreateLayout(tblInfo, SubjectLoadMode.DetailMode);

            if (!IsPostBack)
            {
                base.LoadInstanceData();
                SetButtonAttributes();
            }

            LoadChildLists();
        }

        private void SetButtonAttributes()
        {
            if (AllowModify && CurrentSubject.EditUrl.TrimHasValue())
            {
                btnEdit.Visible = true;
                if (CurrentSubject.IsChildSubject)
                {
                    btnEdit.PostBackUrl = GetUrl(CurrentSubject.EditUrl, CurrentInstance.Id) + string.Format("&{0}={1}", QryMasterInstanceId, MasterSubjectInstanceId);
                }
                else
                {
                    btnEdit.PostBackUrl = GetUrl(CurrentSubject.EditUrl, CurrentInstance.Id);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            lblTitle.Text = GetTitle();
        }

        public void LoadChildLists()
        {
            foreach (SubjectChildListDto childList in CurrentSubject.SubjectChildLists)
            {
                InstanceList childListControl = (InstanceList)Page.LoadControl(ServerPath + InstanceList.ControlURL);
                childListControl.ID = childList.ChildListSubject.SubjectType;
                childListControl.SubjectType = childList.ChildListSubject.SubjectType;
                childListControl.MasterInstance = CurrentInstance;
                childListControl.ControlledFieldName = ControllingFieldName;
                childListControl.ListLabel = childList.GetTitleByLanguage(CurrentUserContext.CurrentLanguage.Id);
                childListControl.AllowAdd = childList.AllowAdd && AllowModify;
                childListControl.AllowEdit = childList.AllowEdit && AllowModify;
                childListControl.AllowDelete = childList.AllowDelete && AllowModify;
                childListControl.AllowImport = childList.AllowImport && AllowModify;
                childListControl.ImportUrl = childList.ImportUrl;
                childListControl.AllowFilteringByColumn = childList.AllowFiltering;
                childListControl.InstanceRowNewing += new InstanceRowNewingEventHandler(childListControl_InstanceRowNewing);
                childListControl.InstanceRowSaving += new InstanceRowSavingEventHandler(childList_InstanceRowSaving);
                childListControl.InstanceRowDeleting += new InstanceRowDeletingEventHandler(childListControl_InstanceRowDeleting);
                childListControl.NeedListInstances += new NeedListInstancesEventHandler(childListControl_NeedListInstances);

                // Add empty row above the list control
                HtmlTable table = new HtmlTable();
                HtmlTableRow row = new HtmlTableRow();
                row.Height = "10";
                table.Rows.Add(row);
                PlaceHolder1.Controls.Add(table);
                // Add the child list control
                PlaceHolder1.Controls.Add(childListControl);
            }
        }

        private void childListControl_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            if (ChildListNeedInstances != null)
            {
                ChildListNeedInstances(this, e);
            }
        }

        private void childListControl_InstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            if (ChildListInstanceRowNewing != null)
            {
                ChildListInstanceRowNewing(this, e);
            }
        }

        private void childList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            if (ChildListInstanceRowSaving != null)
            {
                ChildListInstanceRowSaving(this, e);
            }
        }

        private void childListControl_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            if (ChildListInstanceRowDeleting != null)
            {
                ChildListInstanceRowDeleting(this, e);
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblText.Text = Localize("Common.Subject.UI.InstanceDetail.lblText", "Detail");
            btnEdit.Text = Localize("Common.Subject.UI.InstanceDetail.btnEdit", "Edit");
        }
    }
}
