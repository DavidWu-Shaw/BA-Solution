using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using BA.Core;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common.Helper;
using BA.Web.Common.UserControls;
using Framework.UoW;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;
using Framework.Core;

namespace BA.Web.Common.Subject
{
    public partial class SubjectManager : AdminSetupBasePage
    {
        public const string PageUrl = "/Common/Subject/SubjectManager.aspx";
        public const string QrySubjectId = "SubjectId";

        private const string DataTypeListStateKey = "DataTypeListStateKey";
        private const string SubjectListStateKey = "SubjectListStateKey";
        private const string EntityListStateKey = "EntityListStateKey";
        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstanceStateKey { get { return string.Format("{0}_{1}", UniqueID, InstanceStateKey); } }
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private enum ChildListType
        {
            SubjectFieldInfo,
            SubjectChildList
        }

        private int? SubjectId
        {
            get
            {
                return Request.QueryString[QrySubjectId].TryToParse<int>();
            }
        }

        private SubjectDto _currentSubject;
        public SubjectDto CurrentSubject
        {
            get
            {
                if (_currentSubject == null && IsPostBack && IsInSession(UniqueInstanceStateKey))
                {
                    _currentSubject = GetFromSession(UniqueInstanceStateKey) as SubjectDto;
                }

                return _currentSubject;
            }
            set
            {
                _currentSubject = value;
                SaveInSession(_currentSubject, UniqueInstanceStateKey);
            }
        }

        private IEnumerable<SubjectFieldInfoDto> _subjectFieldInfos;
        public IEnumerable<SubjectFieldInfoDto> SubjectFieldInfos
        {
            get
            {
                if (_subjectFieldInfos == null && IsInSession(UniqueInstancesStateKey))
                {
                    _subjectFieldInfos = GetFromSession(UniqueInstancesStateKey) as IEnumerable<SubjectFieldInfoDto>;
                }

                return _subjectFieldInfos;
            }
            set
            {
                _subjectFieldInfos = value;
                SaveInSession(_subjectFieldInfos, UniqueInstancesStateKey);
            }
        }

        private IEnumerable<BindingListItem> _dataTypeList;
        private IEnumerable<BindingListItem> DataTypeList
        {
            get
            {
                if (_dataTypeList == null)
                {
                    _dataTypeList = (IEnumerable<BindingListItem>)Session[DataTypeListStateKey];
                }
                return _dataTypeList;
            }
            set
            {
                _dataTypeList = value;
                Session[DataTypeListStateKey] = value;
            }
        }

        private IEnumerable<BindingListItem> _subjectList;
        private IEnumerable<BindingListItem> SubjectList
        {
            get
            {
                if (_subjectList == null)
                {
                    _subjectList = (IEnumerable<BindingListItem>)Session[SubjectListStateKey];
                }
                return _subjectList;
            }
            set
            {
                _subjectList = value;
                Session[SubjectListStateKey] = value;
            }
        }

        private IEnumerable<BindingListItem> _entityList;
        private IEnumerable<BindingListItem> EntityList
        {
            get
            {
                if (_entityList == null)
                {
                    _entityList = (IEnumerable<BindingListItem>)Session[EntityListStateKey];
                }
                return _entityList;
            }
            set
            {
                _entityList = value;
                Session[EntityListStateKey] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Subject";

            if (!IsPostBack)
            {
                RetrieveData();
                LoadInstanceData();
            }

            InitChildLists();
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade facade = new SubjectFacade(uow);
                CurrentSubject = facade.RetrieveSubject(SubjectId);
                
                SubjectList = facade.GetBindingList();

                DataTypeFacade dataTypeFacade = new DataTypeFacade(uow);
                DataTypeList = dataTypeFacade.GetBindingList();

                DEntityFacade entityFacade = new DEntityFacade(uow);
                EntityList = entityFacade.GetBindingList();
            }

            RetrieveSubjectFieldInfos();
        }

        private void RetrieveSubject()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade facade = new SubjectFacade(uow);
                CurrentSubject = facade.RetrieveSubject(SubjectId);
            }
        }

        private void RetrieveSubjectFieldInfos()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade subjectFacade = new SubjectFacade(uow);
                SubjectFieldInfos = subjectFacade.RetrieveSubjectFieldInfos(SubjectId);
            }
        }

        private void LoadInstanceData()
        {
            lblTitle.Text = string.Format("Subject: {0}", CurrentSubject.SubjectType);

            lblSubjectType.Text = CurrentSubject.SubjectType;
            lblSubjectLabel.Text = CurrentSubject.SubjectLabel;
            lblSubjectIdField.Text = CurrentSubject.SubjectIdField;
            lblMasterSubjectIdField.Text = CurrentSubject.MasterSubjectIdField;
        }

        private void InitChildLists()
        {
            InitSubjectFieldsList();
            InitSubjectChildListsList();
        }

        private void InitSubjectFieldsList()
        {
            // SubjectField
            ListManager childListControl = (ListManager)Page.LoadControl(ServerPath + ListManager.ControlURL);
            childListControl.ID = ChildListType.SubjectFieldInfo.ToString();
            childListControl.InstanceType = ChildListType.SubjectFieldInfo.ToString();
            childListControl.IsChildList = true;
            childListControl.ListLabel = "Subject Field List";
            childListControl.AllowEditAll = true;
            childListControl.NeedListInstances += new NeedListInstancesEventHandler(childListControl_NeedListInstances);
            childListControl.InstanceRowSaving += new InstanceRowSavingEventHandler(childList_InstanceRowSaving);
            childListControl.InstanceRowDeleting += new InstanceRowDeletingEventHandler(childListControl_InstanceRowDeleting);

            GridViewEditCommandColumn editColumn = new GridViewEditCommandColumn();
            childListControl.Columns.Add(editColumn);

            GridViewDeleteButtonColumn deleteColumn = new GridViewDeleteButtonColumn();
            deleteColumn.ConfirmTextFormatString = "Are you sure to delete {0}";
            deleteColumn.ConfirmTextFields = new string[] { SubjectFieldInfoDto.FLD_TableColumn };
            childListControl.Columns.Add(deleteColumn);

            GridViewDataTextColumn tcc = new GridViewDataTextColumn("Table Column", SubjectFieldInfoDto.FLD_TableColumn);
            tcc.IsReadOnly = true;
            childListControl.Columns.Add(tcc);
            GridViewDataColumn maxC = new GridViewDataColumn("Length In Table", SubjectFieldInfoDto.FLD_MaxLengthInTable);
            maxC.IsReadOnly = true;
            childListControl.Columns.Add(maxC);

            childListControl.Columns.Add(new GridViewDataColumn("Max Length", SubjectFieldInfoDto.FLD_MaxLength));

            childListControl.Columns.Add(new GridViewDataTextColumn("Field Label", SubjectFieldInfoDto.FLD_FieldLabel));
            childListControl.Columns.Add(new GridViewDataTextColumn("Help Text", SubjectFieldInfoDto.FLD_HelpText));
            childListControl.Columns.Add(new GridViewDropDownListColumn("Data Type", SubjectFieldInfoDto.FLD_FieldDataTypeId, DataTypeList));

            GridViewDropDownListColumn lookupCol = new GridViewDropDownListColumn("Lookup Subject", SubjectFieldInfoDto.FLD_LookupSubjectId, SubjectList);
            childListControl.Columns.Add(lookupCol);
            lookupCol.enableEmptyItem = true;

            GridViewDropDownListColumn pickupCol = new GridViewDropDownListColumn("Pickup Entity", SubjectFieldInfoDto.FLD_PickupEntityId, EntityList);
            childListControl.Columns.Add(pickupCol);
            pickupCol.enableEmptyItem = true;

            childListControl.Columns.Add(new GridViewDataColumn("Sort", SubjectFieldInfoDto.FLD_Sort));
            childListControl.Columns.Add(new GridViewDataColumn("Row Index", SubjectFieldInfoDto.FLD_RowIndex));
            childListControl.Columns.Add(new GridViewDataColumn("Col Index", SubjectFieldInfoDto.FLD_ColIndex));
            childListControl.Columns.Add(new GridViewDataCheckColumn("Required", SubjectFieldInfoDto.FLD_IsRequired));
            childListControl.Columns.Add(new GridViewDataCheckColumn("Readonly", SubjectFieldInfoDto.FLD_IsReadonly));
            childListControl.Columns.Add(new GridViewDataCheckColumn("ShowInGrid", SubjectFieldInfoDto.FLD_IsShowInGrid));
            childListControl.Columns.Add(new GridViewDataCheckColumn("LinkInGrid", SubjectFieldInfoDto.FLD_IsLinkInGrid));

            // Add empty row above the list control
            HtmlTable table = new HtmlTable();
            HtmlTableRow row = new HtmlTableRow();
            row.Height = "10";
            table.Rows.Add(row);
            PlaceHolder1.Controls.Add(table);
            // Add the child list control
            PlaceHolder1.Controls.Add(childListControl);
        }

        private void InitSubjectChildListsList()
        {
            // SubjectField
            ListManager childListControl = (ListManager)Page.LoadControl(ServerPath + ListManager.ControlURL);
            childListControl.ID = ChildListType.SubjectChildList.ToString();
            childListControl.InstanceType = ChildListType.SubjectChildList.ToString();
            childListControl.IsChildList = true;
            childListControl.ListLabel = "Subject Child List";
            childListControl.AllowAdd = true;
            childListControl.IsAddInGrid = true;
            childListControl.NeedListInstances += new NeedListInstancesEventHandler(childListControl_NeedListInstances);
            childListControl.InstanceRowNewing += new InstanceRowNewingEventHandler(childListControl_InstanceRowNewing);
            childListControl.InstanceRowSaving += new InstanceRowSavingEventHandler(childList_InstanceRowSaving);
            childListControl.InstanceRowDeleting += new InstanceRowDeletingEventHandler(childListControl_InstanceRowDeleting);

            childListControl.Columns.Add(new GridViewEditCommandColumn());
            GridViewDeleteButtonColumn deleteColumn = new GridViewDeleteButtonColumn();
            deleteColumn.ConfirmTextFormatString = "Are you sure to delete {0}";
            deleteColumn.ConfirmTextFields = new string[] { SubjectChildListDto.FLD_Title };
            childListControl.Columns.Add(deleteColumn);

            childListControl.Columns.Add(new GridViewDataTextColumn("Title", SubjectChildListDto.FLD_Title));
            childListControl.Columns.Add(new GridViewDropDownListColumn("Child Subject", SubjectChildListDto.FLD_ChildListSubjectId, SubjectList));
            childListControl.Columns.Add(new GridViewDataColumn("Sort", SubjectChildListDto.FLD_Sort));
            childListControl.Columns.Add(new GridViewDataCheckColumn("Allow Add", SubjectChildListDto.FLD_AllowAdd));
            childListControl.Columns.Add(new GridViewDataCheckColumn("Allow Edit", SubjectChildListDto.FLD_AllowEdit));
            childListControl.Columns.Add(new GridViewDataCheckColumn("Allow Delete", SubjectChildListDto.FLD_AllowDelete));

            // Add empty row above the list control
            HtmlTable table = new HtmlTable();
            HtmlTableRow row = new HtmlTableRow();
            row.Height = "10";
            table.Rows.Add(row);
            PlaceHolder1.Controls.Add(table);
            // Add the child list control
            PlaceHolder1.Controls.Add(childListControl);
        }

        protected void childListControl_InstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            if (e.InstanceType == ChildListType.SubjectChildList.ToString())
            {
                e.Instance = new SubjectChildListDto();
            }
        }

        protected void childListControl_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            if (e.InstanceType == ChildListType.SubjectFieldInfo.ToString())
            {
                e.Instances = SubjectFieldInfos;
            }
            else if (e.InstanceType == ChildListType.SubjectChildList.ToString())
            {
                e.Instances = CurrentSubject.SubjectChildLists;
            }
        }

        protected void childList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            if (e.InstanceType == ChildListType.SubjectFieldInfo.ToString())
            {
                SubjectFieldInfoDto currentField = e.Instance as SubjectFieldInfoDto;
                IFacadeUpdateResult<SubjectData> result = SaveSubjectField(CurrentSubject.Id, currentField);
                e.IsSuccessful = result.IsSuccessful;
                if (result.IsSuccessful)
                {
                    // Refresh data in session
                    RetrieveSubjectFieldInfos();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
            else if (e.InstanceType == ChildListType.SubjectChildList.ToString())
            {
                SubjectChildListDto childList = e.Instance as SubjectChildListDto;
                IFacadeUpdateResult<SubjectData> result = SaveSubjectChildList(CurrentSubject.Id, childList);
                e.IsSuccessful = result.IsSuccessful;
                if (result.IsSuccessful)
                {
                    // Refresh data in session
                    RetrieveSubject();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected void childListControl_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            if (e.InstanceType == ChildListType.SubjectFieldInfo.ToString())
            {
                SubjectFieldInfoDto currentField = e.Instance as SubjectFieldInfoDto;
                IFacadeUpdateResult<SubjectData> result = DeleteSubjectField(CurrentSubject.Id, currentField.SubjectFieldId);
                e.IsSuccessful = result.IsSuccessful;
                if (result.IsSuccessful)
                {
                    // Refresh data in session
                    RetrieveSubjectFieldInfos();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
            else if (e.InstanceType == ChildListType.SubjectChildList.ToString())
            {
                IFacadeUpdateResult<SubjectData> result = DeleteSubjectChildList(CurrentSubject.Id, e.Instance.Id);
                e.IsSuccessful = result.IsSuccessful;
                if (result.IsSuccessful)
                {
                    // Refresh data in session
                    RetrieveSubject();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private IFacadeUpdateResult<SubjectData> SaveSubjectField(object subjectId, SubjectFieldInfoDto subjectFieldInfo)
        {
            if (subjectFieldInfo.SubjectFieldId == null)
            {
                subjectFieldInfo.FieldKey = subjectFieldInfo.TableColumn;
            }

            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade subjectFacade = new SubjectFacade(uow);
                IFacadeUpdateResult<SubjectData> result = subjectFacade.SaveSubjectField(subjectId, subjectFieldInfo);

                return result;
            }
        }

        private IFacadeUpdateResult<SubjectData> DeleteSubjectField(object subjectId, object subjectFieldId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade subjectFacade = new SubjectFacade(uow);
                IFacadeUpdateResult<SubjectData> result = subjectFacade.DeleteSubjectField(subjectId, subjectFieldId);

                return result;
            }
        }

        private IFacadeUpdateResult<SubjectData> SaveSubjectChildList(object subjectId, SubjectChildListDto subjectChildList)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade subjectFacade = new SubjectFacade(uow);
                IFacadeUpdateResult<SubjectData> result = subjectFacade.SaveSubjectChildList(subjectId, subjectChildList);

                return result;
            }
        }

        private IFacadeUpdateResult<SubjectData> DeleteSubjectChildList(object subjectId, object childListId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade subjectFacade = new SubjectFacade(uow);
                IFacadeUpdateResult<SubjectData> result = subjectFacade.DeleteSubjectChildList(subjectId, childListId);

                return result;
            }
        }
    }
}