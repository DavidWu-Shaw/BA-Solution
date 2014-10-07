using System;
using BA.Core;
using BA.UnityRegistry;
using BA.Web.Common.Helper;
using CRM.Component;
using Framework.Core;
using Framework.UoW;
using SubjectEngine.Component.Dto;
using SubjectEngine.Core;

namespace BA.Web.Common.Subject.UI
{
    public partial class InstanceList : BaseControl
    {
        public const string ControlURL = "/Common/Subject/UI/InstanceList.ascx";
        private const string QryMasterInstanceId = "MasterInstanceId";

        private const string SubjectStateKey = "SubjectSessionKey";
        private string UniqueSubjectStateKey { get { return string.Format("{0}_{1}", UniqueID, SubjectStateKey); } }

        public event NeedListInstancesEventHandler NeedListInstances;
        public event InstanceRowNewingEventHandler InstanceRowNewing;
        public event InstanceRowSavingEventHandler InstanceRowSaving;
        public event InstanceRowDeletingEventHandler InstanceRowDeleting;

        public string ListLabel { get; set; }
        public bool AllowFilteringByColumn { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowImport { get; set; }
        public string ImportUrl { get; set; }

        // Controlled by other subject and instance
        public BaseDto MasterInstance { get; set; }
        public string ControlledFieldName { get; set; }

        public string SubjectType { get; set; }

        private SubjectDto _currentSubject;
        public SubjectDto CurrentSubject
        {
            get
            {
                if (_currentSubject == null)
                {
                    if (IsInSession(UniqueSubjectStateKey))
                    {
                        _currentSubject = GetFromSession(UniqueSubjectStateKey) as SubjectDto;
                    }
                    else
                    {
                        _currentSubject = WebContext.Current.GetSubject(SubjectType);
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            CRMSubjectFacade pa = new CRMSubjectFacade(uow);
                            pa.AttachProperties(_currentSubject);
                        }

                        SaveInSession(_currentSubject, UniqueSubjectStateKey);
                    }
                }

                return _currentSubject;
            }
            set
            {
                _currentSubject = value;
                SaveInSession(_currentSubject, UniqueSubjectStateKey);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                RemoveFromSession(UniqueSubjectStateKey);
            }

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitListManager();
        }

        protected void ucListManager_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            if (InstanceRowDeleting != null)
            {
                InstanceRowDeleting(this, e);
            }
        }

        protected void ucListManager_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            if (InstanceRowSaving != null)
            {
                InstanceRowSaving(this, e);
            }
        }

        protected void ucListManager_InstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            if (InstanceRowNewing != null)
            {
                InstanceRowNewing(this, e);
            }
        }

        protected void ucListManager_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            if (NeedListInstances != null)
            {
                NeedListInstances(this, e);
            }
        }

        private void InitListManager()
        {
            ucListManager.ID = string.Format("{0}List", CurrentSubject.SubjectType);
            ucListManager.InstanceType = CurrentSubject.SubjectType;
            ucListManager.IsGridInFormEdit = CurrentSubject.IsGridInFormEdit;

            if (ControlledFieldName.TrimHasValue() && !object.Equals(CurrentSubject.MasterSubjectIdField, ControlledFieldName))
            {
                AllowAdd = false;
                AllowEdit = false;
                AllowDelete = false;
                AllowImport = false;
            }

            if (MasterInstance != null)
            {
                // Child list
                ucListManager.IsChildList = true;
                ucListManager.IsAddInGrid = true;
                if (AllowImport && ImportUrl.TrimHasValue())
                {
                    ucListManager.ImportBtnPostBackUrl = GetUrl(ImportUrl, MasterInstance.Id);
                }
            }
            else
            {
                // Top level list
                ucListManager.IsChildList = false;
                ucListManager.IsAddInGrid = CurrentSubject.IsAddInGrid;
                if (AllowAdd && CurrentSubject.EditUrl.TrimHasValue())
                {
                    ucListManager.AddBtnPostBackUrl = GetUrl(CurrentSubject.EditUrl, null);
                }
                if (AllowImport && CurrentSubject.ImportUrl.TrimHasValue())
                {
                    ucListManager.ImportBtnPostBackUrl = GetUrl(CurrentSubject.ImportUrl, null);
                }
            }
            
            ucListManager.ListLabel = ListLabel;
            ucListManager.AllowFilteringByColumn = AllowFilteringByColumn;
            ucListManager.AllowAdd = AllowAdd;
            ucListManager.AllowImport = AllowImport;

            ucListManager.NeedListInstances += new NeedListInstancesEventHandler(ucListManager_NeedListInstances);
            ucListManager.InstanceRowNewing += new InstanceRowNewingEventHandler(ucListManager_InstanceRowNewing);
            ucListManager.InstanceRowSaving += new InstanceRowSavingEventHandler(ucListManager_InstanceRowSaving);
            ucListManager.InstanceRowDeleting += new InstanceRowDeletingEventHandler(ucListManager_InstanceRowDeleting);

            DefineColumns();
        }

        private void DefineColumns()
        {
            foreach (SubjectFieldDto field in CurrentSubject.SubjectFields)
            {
                if (field.IsShowInGrid)
                {
                    if (field.FieldKey.Equals(ControlledFieldName, StringComparison.OrdinalIgnoreCase))
                        continue;
                    // Todo: implement multi-language
                    string headerText = field.GetFieldLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
                    if (field.IsLinkInGrid && field.DucType != DucTypes.Lookup && field.DucType != DucTypes.Pickup)
                    {
                        GridViewLinkButtonColumn hc = new GridViewLinkButtonColumn(headerText, field.FieldKey);
                        ucListManager.Columns.Add(hc);
                        hc.EditColumnIndex = field.ColIndex;
                        hc.DataNavigateUrlFormatString = ServerPath + field.LookupSubjectManageUrlFormatString;
                        if (field.DucType == DucTypes.Lookup && field.LookupSubjectId != null)
                        {
                            // Link to another subject by LookupSubjectId
                            hc.DataNavigateUrlField = field.FieldKey;
                        }
                        else
                        {
                            // Link to current subject
                            if (MasterInstance != null)
                            {
                                // Add MasterInstance
                                hc.DataNavigateUrlFormatString += string.Format("&{0}={1}", QryMasterInstanceId, MasterInstance.Id);
                            }
                            hc.DataNavigateUrlField = BaseDto.FLD_StringId;
                        }
                    }
                    else
                    {
                        switch (field.DucType)
                        {
                            case DucTypes.Lookup:
                            case DucTypes.Pickup:
                                GridViewDropDownListColumn ddlCol = new GridViewDropDownListColumn(headerText, field.FieldKey, field.ListDataSource);
                                ucListManager.Columns.Add(ddlCol);
                                ddlCol.IsReadOnly = field.IsReadonly;
                                ddlCol.enableEmptyItem = !field.IsRequired;
                                ddlCol.EditColumnIndex = field.ColIndex;
                                ddlCol.DropDownHeight = WebContext.Current.ApplicationOption.DropDownHeight;
                                break;
                            case DucTypes.Text:
                            case DucTypes.TextArea:
                            case DucTypes.Currency:
                            case DucTypes.Email:
                            case DucTypes.Formula:
                            case DucTypes.Integer:
                            case DucTypes.Number:
                            case DucTypes.Percentage:
                                GridViewDataTextColumn textCol = new GridViewDataTextColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(textCol);
                                textCol.IsReadOnly = field.IsReadonly;
                                textCol.EditColumnIndex = field.ColIndex;
                                if (field.MaxLength.HasValue)
                                {
                                    textCol.MaxLength = field.MaxLength.Value;
                                }
                                break;
                            case DucTypes.Date:
                                GridViewDataDateColumn dateCol = new GridViewDataDateColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(dateCol);
                                dateCol.IsReadOnly = field.IsReadonly;
                                dateCol.EditColumnIndex = field.ColIndex;
                                break;
                            case DucTypes.Datetime:
                                GridViewDataDateTimeColumn dateTimeCol = new GridViewDataDateTimeColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(dateTimeCol);
                                dateTimeCol.IsReadOnly = field.IsReadonly;
                                dateTimeCol.EditColumnIndex = field.ColIndex;
                                break;
                            case DucTypes.Time:
                                GridViewDataTimeColumn timeCol = new GridViewDataTimeColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(timeCol);
                                timeCol.IsReadOnly = field.IsReadonly;
                                timeCol.EditColumnIndex = field.ColIndex;
                                break;
                            case DucTypes.Image:
                                GridViewBinaryImageColumn ic = new GridViewBinaryImageColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(ic);
                                ic.IsReadOnly = field.IsReadonly;
                                ic.EditColumnIndex = field.ColIndex;
                                ic.ImageWidth = 60;
                                ic.ImageHeight = 80;
                                break;
                            case DucTypes.Attachment:
                                GridViewAttachmentColumn ac = new GridViewAttachmentColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(ac);
                                ac.IsReadOnly = field.IsReadonly;
                                ac.EditColumnIndex = field.ColIndex;
                                ac.NavigateUrlFormatString = ServerPath + field.NavigateUrlFormatString;
                                ac.NavigateUrlField = BaseDto.FLD_StringId;
                                ac.LinkTextField = BA.Core.UIConst.FLD_LinkText;
                                break;
                            default:
                                GridViewDataColumn c = new GridViewDataColumn(headerText, field.FieldKey);
                                ucListManager.Columns.Add(c);
                                c.IsReadOnly = field.IsReadonly;
                                c.EditColumnIndex = field.ColIndex;
                                if (field.MaxLength.HasValue)
                                {
                                    c.MaxLength = field.MaxLength.Value;
                                }
                                break;
                        }
                    }
                }
            }

            if (AllowEdit)
            {
                GridViewEditCommandColumn editColumn = new GridViewEditCommandColumn();
                ucListManager.Columns.Add(editColumn);
                editColumn.UniqueName = "edt0001";
                editColumn.NonExportable = true;
            }
            if (AllowDelete)
            {
                GridViewDeleteButtonColumn deleteColumn = new GridViewDeleteButtonColumn();
                ucListManager.Columns.Add(deleteColumn);
                deleteColumn.UniqueName = "del0001";
                deleteColumn.NonExportable = true;
                deleteColumn.ConfirmTextFormatString = "Are you sure to delete {0}";
                deleteColumn.ConfirmTextFields = new string[] { BaseDto.FLD_Display };
            }
        }
    }
}