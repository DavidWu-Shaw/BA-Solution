using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using BA.Web.Common.Helper;
using Framework.Core;
using Framework.Core.Helpers;
using Telerik.Web.UI;

namespace BA.Web.Common.UserControls
{
    public partial class ListManager : BaseControl
    {
        public const string ControlURL = "/Common/UserControls/ListManager.ascx";

        public string InstanceType { get; set; }
        public string ListLabel { get; set; }
        public List<GridViewColumn> Columns { get; private set; }
        public bool AllowFilteringByColumn { get; set; }
        public string KeyFieldName { get; set; }
        public bool IsChildList { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowImport { get; set; }
        public bool IsAddInGrid { get; set; }
        public bool IsGridInFormEdit { get; set; }
        public bool AllowEditAll { get; set; }
        public int? GridPageSize { get; set; }

        public string AddBtnPostBackUrl { get; set; }
        public string ImportBtnPostBackUrl { get; set; }

        public event NeedListInstancesEventHandler NeedListInstances;
        public event InstanceRowNewingEventHandler InstanceRowNewing;
        public event InstanceRowSavingEventHandler InstanceRowSaving;
        public event InstanceRowDeletingEventHandler InstanceRowDeleting;

        private RadGrid RadGrid1;
        private bool IsEnteringEditAll;
        private bool IsQuitingEditAll;

        public ListManager()
        {
            KeyFieldName = BaseDto.FLD_StringId;
            Columns = new List<GridViewColumn>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddGrid();
            InitTitleBar();
        }

        private void InitTitleBar()
        {
            InitButtons();

            if (!IsPostBack)
            {
                if (IsChildList)
                {
                    tdTitle.Attributes.Add("class", "devSubTitle");
                }
                else
                {
                    tdTitle.Attributes.Add("class", "DarkTitle");
                }

                lblTitle.Text = ListLabel;
            }
        }

        private void InitButtons()
        {
            if (AllowAdd)
            {
                tdAddRecord.Visible = true;

                if (IsAddInGrid)
                {
                    btnAddRecord.Attributes.Add("onclick", string.Format("AddNewItem('{0}'); return false;", RadGrid1.ClientID));
                }
                else
                {
                    btnAddRecord.PostBackUrl = AddBtnPostBackUrl;
                }
            }

            if (AllowImport)
            {
                tdImport.Visible = true;
                btnImport.PostBackUrl = ImportBtnPostBackUrl;
            }

            if (AllowEditAll)
            {
                RadGrid1.AllowMultiRowEdit = true;
                tdEditAll.Visible = true;
                tdSaveAll.Visible = true;
                tdCancelAll.Visible = true;
                btnSaveAll.Click += new ImageClickEventHandler(btnSaveAll_Click);
                btnCancelAll.Click += new ImageClickEventHandler(btnCancelAll_Click);
                btnEditAll.Click += new ImageClickEventHandler(btnEditAll_Click);
            }
        }

        private void AddGrid()
        {
            RadGrid1 = new RadGrid();
            RadGrid1.ID = InstanceType;
            int pageSize = WebContext.Current.ApplicationOption.GridPageSize;
            if (GridPageSize.HasValue)
            {
                pageSize = GridPageSize.Value;
            }
            RadGridHelper.SetStyle(RadGrid1, pageSize);

            RadGrid1.MasterTableView.AllowFilteringByColumn = AllowFilteringByColumn;
            if (IsGridInFormEdit)
            {
                RadGrid1.MasterTableView.EditMode = GridEditMode.EditForms;
                RadGrid1.MasterTableView.EditFormSettings.ColumnNumber = WebContext.Current.ApplicationOption.EditFormColumnMax;
            }

            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
            RadGrid1.UpdateCommand += new GridCommandEventHandler(RadGrid1_UpdateCommand);
            RadGrid1.InsertCommand += new GridCommandEventHandler(RadGrid1_InsertCommand);
            RadGrid1.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);
            RadGrid1.DeleteCommand += new GridCommandEventHandler(RadGrid1_DeleteCommand);
            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
            RadGrid1.PreRender += new EventHandler(RadGrid1_PreRender);

            DefineColumns();
            PlaceHolder1.Controls.Add(RadGrid1);
        }

        private void DefineColumns()
        {
            RadGrid1.MasterTableView.DataKeyNames = new string[] { KeyFieldName };

            RadGrid1.MasterTableView.Columns.Clear();
            foreach (GridViewColumn column in Columns)
            {
                GridColumn radColumn = column.CreateColumn();
                RadGrid1.MasterTableView.Columns.Add(radColumn);
                column.AttachProperties(radColumn);
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid grid = (RadGrid)sender;
            grid.DataSource = GetInstances();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                foreach (GridViewColumn column in Columns)
                {
                    if (column is GridViewDropDownListColumn)
                    {
                        // Binding DDL type fields
                        GridViewDropDownListColumn ddlColumn = column as GridViewDropDownListColumn;
                        if (!ddlColumn.IsReadOnly)
                        {
                            GridTemplateColumnEditor templateEditor = (GridTemplateColumnEditor)editMan.GetColumnEditor(ddlColumn.DataFieldKey);
                            RadComboBox ddl = templateEditor.ContainerControl.FindControl(ddlColumn.DataFieldKey) as RadComboBox;
                            if (ddlColumn.enableEmptyItem)
                            {
                                ddl.Items.Add(new RadComboBoxItem(BindingListItem.EmptyText, BindingListItem.EmptyValue));
                                ddl.AppendDataBoundItems = true;
                            }
                            ddl.DataSource = ddlColumn.ListDataSource;
                            ddl.DataValueField = BindingListItem.ValueProperty;
                            ddl.DataTextField = BindingListItem.TextProperty;
                            ddl.DataBind();
                            // Set current value
                            //if (e.Item is GridDataInsertItem || e.Item is GridEditFormInsertItem)
                            if (e.Item is IGridInsertItem)
                            {
                                ddl.SelectedValue = null;
                            }
                            else
                            {
                                object currentValue = DataBinder.Eval(e.Item.DataItem, ddlColumn.DataFieldKey);
                                if (currentValue != null)
                                {
                                    ddl.SelectedValue = currentValue.ToString();
                                }
                            }
                        }
                    }
                    else if (column is GridViewLinkButtonColumn)
                    {
                        // Binding Link button type fields
                        GridViewLinkButtonColumn linkColumn = column as GridViewLinkButtonColumn;
                        if (!linkColumn.IsReadOnly)
                        {
                            GridTemplateColumnEditor templateEditor = (GridTemplateColumnEditor)editMan.GetColumnEditor(linkColumn.DataTextField);
                            RadTextBox textBox = templateEditor.ContainerControl.FindControl(linkColumn.DataTextField) as RadTextBox;
                            
                            // Set current value
                            if (e.Item is IGridInsertItem)
                            {
                                textBox.Text = null;
                            }
                            else
                            {
                                object currentValue = DataBinder.Eval(e.Item.DataItem, linkColumn.DataTextField);
                                if (currentValue != null)
                                {
                                    textBox.Text = currentValue.ToString();
                                }
                            }
                        }
                    }
                    else if (column is GridViewDataDateColumn)
                    {

                    }
                }
            }
        }

        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            //Update new values
            OrderedDictionary newValues = new OrderedDictionary();
            //The GridTableView will fill the values from all editable columns in the hash
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);

            int id = Int32.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][KeyFieldName].ToString());
            IEnumerable<BaseDto> instances = GetInstances();
            if (instances != null)
            {
                BaseDto instance = instances.FirstOrDefault(o => object.Equals(o.Id, id));
                if (instance != null)
                {
                    SaveRow(instance, newValues);
                }
            }
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            //Update new values
            OrderedDictionary newValues = new OrderedDictionary();
            //The GridTableView will fill the values from all editable columns in the hash
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);

            InstanceRowNewingEventArgs arg = new InstanceRowNewingEventArgs(InstanceType);
            if (InstanceRowNewing != null)
            {
                InstanceRowNewing(this, arg);
            }

            if (arg.Instance != null)
            {
                SaveRow(arg.Instance, newValues);
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            int id = Int32.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][KeyFieldName].ToString());
            IEnumerable<BaseDto> instances = GetInstances();
            if (instances != null)
            {
                BaseDto instance = instances.FirstOrDefault(o => object.Equals(o.Id, id));
                if (instance != null)
                {
                    DeleteRow(instance);
                }
            }
        }

        private void DeleteRow(BaseDto instance)
        {
            InstanceRowDeletingEventArgs arg = new InstanceRowDeletingEventArgs(instance, InstanceType);
            if (InstanceRowDeleting != null)
            {
                InstanceRowDeleting(this, arg);
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //switch the edit/insert modes to ensure that only one operation will be processed at given time
            if (e.CommandName == RadGrid.EditCommandName)
            {
                e.Item.OwnerTableView.IsItemInserted = false;
            }
            else if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid1.EditIndexes.Clear();
            }
        }

        protected void btnEditAll_Click(object sender, ImageClickEventArgs e)
        {
            IsEnteringEditAll = true;
        }

        protected void btnSaveAll_Click(object sender, ImageClickEventArgs e)
        {
            IEnumerable<BaseDto> instances = GetInstances();

            if (instances != null)
            {
                foreach (GridEditableItem editedItem in RadGrid1.EditItems)
                {
                    //Update new values
                    OrderedDictionary newValues = new OrderedDictionary();
                    //The GridTableView will fill the values from all editable columns in the hash
                    editedItem.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);
                    int id = Int32.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][KeyFieldName].ToString());
                    BaseDto instance = instances.FirstOrDefault(o => object.Equals(o.Id, id));
                    if (instance != null)
                    {
                        SaveRow(instance, newValues);
                    }
                }
            }

            IsQuitingEditAll = true;
        }

        protected void btnCancelAll_Click(object sender, ImageClickEventArgs e)
        {
            IsQuitingEditAll = true;
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (IsEnteringEditAll)
            {
                EditAllRows();
            }

            if (IsQuitingEditAll)
            {
                QuitEditAllMode();
            }
        }

        private void EditAllRows()
        {
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridEditableItem)
                {
                    GridEditableItem editableItem = item as GridDataItem;
                    editableItem.Edit = true;
                }
            }

            RadGrid1.Rebind();
        }

        private void QuitEditAllMode()
        {
            foreach (GridEditableItem editedItem in RadGrid1.EditItems)
            {
                editedItem.Edit = false;
            }

            RadGrid1.Rebind();
        }

        private void SaveRow(BaseDto instance, OrderedDictionary fieldKeyValues)
        {
            foreach (string prop in fieldKeyValues.Keys)
            {
                ReflectionHelper.SetValue(instance, prop, fieldKeyValues[prop]);
            }

            InstanceRowSavingEventArgs arg = new InstanceRowSavingEventArgs(instance, InstanceType);
            if (InstanceRowSaving != null)
            {
                InstanceRowSaving(this, arg);
            }
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.FileName = string.Format("{0}List", InstanceType);

            // Hide NonExportable columns
            foreach (GridViewColumn column in Columns)
            {
                if (column.NonExportable && column.UniqueName.TrimHasValue())
                {
                    RadGrid1.Columns.FindByUniqueName(column.UniqueName).Visible = false;
                }
            }
            // Export
            RadGrid1.MasterTableView.ExportToExcel();
        }

        private IEnumerable<BaseDto> GetInstances()
        {
            NeedListInstancesEventArgs arg = new NeedListInstancesEventArgs(InstanceType);
            if (NeedListInstances != null)
            {
                NeedListInstances(this, arg);
            }

            return arg.Instances;
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblAdd.Text = Localize("Common.UserControls.ListManager.lblAdd", "Add Record");
            lblImport.Text = Localize("Common.UserControls.ListManager.lblImport", "Import");
            lblExport.Text = Localize("Common.UserControls.ListManager.lblExport", "Export");
            lblEditAll.Text = Localize("Common.UserControls.ListManager.lblEditAll", "Edit All");
            lblSaveAll.Text = Localize("Common.UserControls.ListManager.lblSaveAll", "Save All");
            lblCancelAll.Text = Localize("Common.UserControls.ListManager.lblCancelAll", "Cancel All");
        }
    }
}