using System;
using ExcelTool;
using System.Collections.Generic;
using Tools.Office.Excel;
using General.Utility.Excel;
using System.Web.UI.WebControls;
using System.Data;
using BA.Web.Common.Helper;

namespace BA.Web.Common.UserControls
{
    public partial class ExcelImport : BaseControl
    {
        private const string Col_Row = "Row";
        private const string Col_Validation = "Validation";
        /// <summary>
        /// Occurs when parse Excel fails.
        /// </summary>
        public event ExcelParseEventHandler ExcelParsing;
        public event ExcelSheetValidatingEventHandler ExcelSheetValidating;
        public event ExcelSheetRowValidatingEventHandler ExcelSheetRowValidating;
        /// <summary>
        /// Occurs when Excel file data imported.
        /// </summary>
        public event ExcelImportEventHandler ExcelImported;

        private const string UploadedDataSessionKey = "UploadedDataSessionKey";
        private const string MappingListStateKey = "MappingListStateKey";

        private Dictionary<string, ExcelSheet> _uploadedSheets;
        private Dictionary<string, ExcelSheet> UploadedSheets
        {
            get
            {
                if (_uploadedSheets == null)
                {
                    if (IsInSession(UploadedDataSessionKey))
                    {
                        _uploadedSheets = GetFromSession(UploadedDataSessionKey) as Dictionary<string, ExcelSheet>;
                    }
                }

                return _uploadedSheets;
            }
            set
            {
                _uploadedSheets = value;
                SaveInSession(value, UploadedDataSessionKey);
                FillDDLExcelSheets(_uploadedSheets);
            }
        }

        private ExcelSheet CurrentSelectedSheet
        {
            get
            {
                if (ddlSheetName.SelectedItem != null && UploadedSheets != null)
                {
                    string sheetName = ddlSheetName.SelectedItem.Value.ToString();

                    return UploadedSheets[sheetName];
                }
                else
                {
                    return null;
                }
            }
        }

        private List<ImportFieldMapping> _mappingList;
        public List<ImportFieldMapping> MappingList
        {
            get
            {
                if (_mappingList == null)
                {
                    if (IsInSession(MappingListStateKey))
                    {
                        _mappingList = GetFromSession(MappingListStateKey) as List<ImportFieldMapping>;
                    }
                }

                return _mappingList;
            }
            set
            {
                _mappingList = value;
                SaveInSession(value, MappingListStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearUploadedSheets();
                InitPreviewGrid();
            }
        }

        private bool UploadAndImportExcelFile()
        {
            if (excelFileUpload.HasFile)
            {
                ClearUploadedSheets();

                // Parse and read data from excel file
                try
                {
                    ExcelParser parser = new ExcelParser(excelFileUpload.FileContent, excelFileUpload.FileName);
                    Workbook workbook = parser.GetWorkBook();
                    UploadedSheets = GetSheets(workbook);
                }
                catch (Exception ex)
                {
                    ExcelParseEventArgs arg = new ExcelParseEventArgs();
                    arg.IsSuccessful = false;
                    arg.Message = string.Format("{0}  Please check file format.", ex.Message);
                    if (ExcelParsing != null)
                    {
                        ExcelParsing(this, arg);
                    }

                    return false;
                }

                return true;
            }
            else
            {
                AlertMessages("Please input excel file.");
                return false;
            }
        }

        private void ClearUploadedSheets()
        {
            UploadedSheets = null;
        }

        private void DisplayRawSheetData()
        {
            CurrentSelectedSheet.ResetHeader();
            gvRaw.DataSource = CurrentSelectedSheet.GetSampleData();
            gvRaw.DataBind();
        }

        private void InitMappingGrid()
        {
            CurrentSelectedSheet.ResetHeader();
            // Get Header Row Index 
            int? headerRowNo = null;
            if (gvRaw.SelectedIndexes.Count > 0)
            {
                headerRowNo = Convert.ToInt32(gvRaw.SelectedIndexes[0]);
            }
            // Display 5 rows of sample data
            int rowIndex = 5;
            if (headerRowNo.HasValue)
            {
                rowIndex += headerRowNo.Value + 1;
            }
            gvSample.DataSource = CurrentSelectedSheet.GetSampleData(rowIndex);
            gvSample.DataBind();

            if (headerRowNo.HasValue)
            {
                int[] selectedIndexes = new int[1];
                selectedIndexes[0] = headerRowNo.Value;
                gvSample.SelectedIndexes.Add(selectedIndexes);

                // Set Excel sheet header based on user input
                CurrentSelectedSheet.SetHeaderByRow(headerRowNo.Value);
            }

            // Auto mapping by comparing 
            AutoMatchFields();

            rptItems.DataSource = MappingList;
            rptItems.DataBind();
        }

        protected void rptItems_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            DropDownList ddl = e.Item.FindControl("ddlColumns") as DropDownList;

            if (ddl != null)
            {
                ddl.Items.Add(new ListItem(string.Empty, string.Empty));
                foreach (ExcelSheetColumn col in CurrentSelectedSheet.Columns)
                {
                    ddl.Items.Add(new ListItem(col.Name, col.Name));
                }

                ImportFieldMapping mapping = e.Item.DataItem as ImportFieldMapping;
                if (mapping != null)
                {
                    ddl.SelectedValue = mapping.ColumnName;
                }
            }
        }

        private void CollectMappingInfo()
        {
            for (int index = 0; index < MappingList.Count; index++)
            {
                RepeaterItem item = rptItems.Items[index];
                ImportFieldMapping mapping = MappingList[index];

                DropDownList ddl = item.FindControl("ddlColumns") as DropDownList;
                if (ddl != null)
                {
                    if (ddl.SelectedValue != null)
                    {
                        mapping.ColumnName = ddl.SelectedValue;
                    }
                }
            }
        }

        private bool CreateResultSheet()
        {
            CurrentSelectedSheet.ResetMapping();
            // Check to see if all required mapping are mapped
            bool isValid = ValidateMapping();
            if (isValid)
            {
                ApplyMappingInfoAndValidate(CurrentSelectedSheet);
                DisplayImportedExcelSheet(CurrentSelectedSheet);
            }

            return isValid;
        }

        private bool SubmitResultSheet()
        {
            ExcelImportEventArgs arg = new ExcelImportEventArgs();

            if (CurrentSelectedSheet.IsValid)
            {
                arg.Result = CurrentSelectedSheet;

                if (ExcelImported != null)
                {
                    ExcelImported(this, arg);
                }

                AlertMessages(arg.Message);
                if (!arg.Result.IsValid)
                {
                    DisplayImportedExcelSheet(arg.Result);
                }
            }
            else
            {
                AlertMessages(string.Format("Invalid data found: {0} rows. \\nPlease fix the errors and import again. ", CurrentSelectedSheet.InvalidRowCount));
            }

            return arg.IsSuccessful;
        }

        /// <summary>
        ///  Init the ImportMappingList according to ImportedData
        /// </summary>
        private void AutoMatchFields()
        {
            // Create dictionary of colums using uppercases
            Dictionary<string, ExcelSheetColumn> dic = new Dictionary<string, ExcelSheetColumn>();
            foreach (ExcelSheetColumn column in CurrentSelectedSheet.Columns)
            {
                dic.Add(column.Name.ToUpper(), column);
            }
            // Do matching and mapping
            foreach (ImportFieldMapping mapping in MappingList)
            {
                string requestField = mapping.RequestField.ToUpper();
                if (dic.ContainsKey(requestField))
                {
                    ExcelSheetColumn column = dic[requestField];
                    mapping.ColumnName = column.Name;
                }
                else
                {
                    mapping.ColumnName = null;
                }
            }
        }

        private void FillDDLExcelSheets(Dictionary<string, ExcelSheet> sheets)
        {
            if (sheets != null)
            {
                foreach (string sheetName in sheets.Keys)
                {
                    ddlSheetName.Items.Add(sheetName);
                }

                ddlSheetName.SelectedIndex = 0;
            }
            else
            {
                ddlSheetName.Items.Clear();
                ddlSheetName.SelectedIndex = -1;
            }
        }

        private bool ValidateMapping()
        {
            // Collect mapping info from user input
            CollectMappingInfo();

            bool isValid = true;
            int mappedCount = 0;

            foreach (ImportFieldMapping mapping in MappingList)
            {
                if (mapping.IsMust && string.IsNullOrEmpty(mapping.ColumnName))
                {
                    AlertMessages(string.Format("Missing field [{0}]. Please check excel sheet columns.", mapping.RequestField));
                    isValid = false;
                    break;
                }

                if (mapping.ColumnName != null)
                {
                    mappedCount++;
                }
            }

            if (isValid && mappedCount == 0)
            {
                isValid = false;
                AlertMessages("No column found. Please finish mapping. ");
            }

            return isValid;
        }

        private void ApplyMappingInfoAndValidate(ExcelSheet sheet)
        {
            // Map columns according to mapping list
            foreach (ImportFieldMapping mapping in MappingList)
            {
                if (!string.IsNullOrEmpty(mapping.ColumnName))
                {
                    ExcelSheetColumn column = sheet.Columns[mapping.ColumnName];
                    column.TargetName = mapping.RequestField;
                    column.Validator = mapping.Validator;
                }
            }

            // Clear all row-level validate results
            foreach (ExcelSheetRow row in sheet.Rows)
            {
                row.RowLevelValidateResult.Clear();
            }
            // Validate whole sheet
            // Trigger SheetValidate event
            ExcelSheetValidatingEventArgs arg = new ExcelSheetValidatingEventArgs(sheet);
            if (ExcelSheetValidating != null)
            {
                ExcelSheetValidating(this, arg);
            }

            if (!arg.IsCancelled)
            {
                // Validate each row
                for (int index = sheet.DataStartRowIndex; index < sheet.Rows.Count; index++)
                {
                    ExcelSheetRow row = sheet.Rows[index];
                    // trigger RowValidate event
                    ExcelSheetRowValidatingEventArgs args = new ExcelSheetRowValidatingEventArgs(row);
                    if (ExcelSheetRowValidating != null)
                    {
                        ExcelSheetRowValidating(this, args);
                    }

                    if (!args.IsCancelled)
                    {
                        // Validate each cell in current row
                        foreach (ExcelSheetCell cell in row.Cells)
                        {
                            cell.Validate();
                        }
                    }
                }
            }
        }

        private void InitPreviewGrid()
        {
            gvPreview.KeyFieldName = Col_Row;
            gvPreview.Columns.Clear();
            gvPreview.Columns.Add(new GridViewDataTextColumn(Col_Row));
            gvPreview.Columns.Add(new GridViewDataTextColumn(Col_Validation));
            // Map columns according to mapping list
            foreach (ImportFieldMapping mapping in MappingList)
            {
                gvPreview.Columns.Add(new GridViewDataTextColumn(mapping.RequestField));
            }
        }

        private void DisplayImportedExcelSheet(ExcelSheet importedSheet)
        {
            DataTable importedData = GetSheetDataTable(importedSheet);
            gvPreview.DataSource = importedData;
            //gvPreview.DataSourceRowCount = importedData.Rows.Count;
            gvPreview.GridDataBind();
        }

        private DataTable GetSheetDataTable(ExcelSheet sheet)
        {
            DataTable table = new DataTable(sheet.Name);

            table.Columns.Add(Col_Row);
            table.Columns.Add(Col_Validation);
            foreach (ImportFieldMapping mapping in MappingList)
            {
                table.Columns.Add(mapping.RequestField);
            }

            for (int index = sheet.DataStartRowIndex; index < sheet.Rows.Count; index++)
            {
                ExcelSheetRow row = sheet.Rows[index];
                DataRow dataRow = table.NewRow();
                table.Rows.Add(dataRow);

                dataRow[Col_Row] = row.RowNo;
                dataRow[Col_Validation] = row.ValidateResultString;

                int colIndex = 0;
                foreach (ExcelSheetColumn column in sheet.Columns)
                {
                    if (column.IsMapped)
                    {
                        dataRow[column.TargetName] = row[colIndex].Value;
                    }
                    colIndex++;
                }
            }

            return table;
        }

        private Dictionary<string, ExcelSheet> GetSheets(Workbook workbook)
        {
            Dictionary<string, ExcelSheet> sheets = new Dictionary<string, ExcelSheet>();

            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                Worksheet aSheet = workbook.Worksheets[i];
                ExcelSheet sheet = GetSheet(aSheet);
                sheets.Add(sheet.Name, sheet);
            }

            return sheets;
        }

        private ExcelSheet GetSheet(Worksheet workSheet)
        {
            // get the row count and column count of excel sheet
            int rowCounts = workSheet.MaxRowIndex + 1;
            int colCounts = workSheet.MaxColIndex + 1;

            ExcelSheet sheet = new ExcelSheet(workSheet.Name);
            // Create table schema according the excel sheet
            for (int colIndex = 0; colIndex < colCounts; colIndex++)
            {
                string columnName = string.Format("F{0}", colIndex + 1);
                sheet.Columns.Add(columnName);
            }

            // Read data from each cell, store them to table
            for (int rowIndex = 0; rowIndex < rowCounts; rowIndex++)
            {
                ExcelSheetRow row = sheet.NewRow();
                row.RowNo = rowIndex + 1;

                // copy data for current row
                for (int colIndex = 0; colIndex < colCounts; colIndex++)
                {
                    Cell aCell = workSheet.Cells[rowIndex, colIndex];
                    if (aCell != null)
                    {
                        row[colIndex].Value = aCell.StringValue;
                    }
                    else
                    {
                        row[colIndex].Value = string.Empty;
                    }
                }

                bool isNull = IsNull(row);
                // add row to sheet if it's not null row
                if (!isNull)
                {
                    sheet.Rows.Add(row);
                }
            }

            return sheet;
        }

        private bool IsNull(ExcelSheetRow row)
        {
            bool isNull = true;

            foreach (ExcelSheetCell cell in row.Cells)
            {
                if (cell.Value != null && cell.Value.Length > 0)
                {
                    isNull = false;
                    break;
                }
            }

            return isNull;
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.NextStepIndex == 1)
            {
                bool isSuccessful = UploadAndImportExcelFile();
                if (!isSuccessful)
                {
                    e.Cancel = true;
                }
            }
            else if (e.NextStepIndex == 2)
            {
                if (CurrentSelectedSheet != null)
                {
                    DisplayRawSheetData();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.NextStepIndex == 3)
            {
                if (CurrentSelectedSheet != null)
                {
                    InitMappingGrid();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.NextStepIndex == 4)
            {
                // Create result sheet according to current sheet and mapping info
                bool isSuccessful = CreateResultSheet();
                if (!isSuccessful)
                {
                    e.Cancel = true;
                }
            }
        }

        protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            bool isSuccessful = SubmitResultSheet();
            if (!isSuccessful)
            {
                e.Cancel = true;
            }
        }

        protected void Wizard1_PreRender(object sender, EventArgs e)
        {
            Repeater steps = Wizard1.FindControl("HeaderContainer").FindControl("rptSteps") as Repeater;

            steps.DataSource = Wizard1.WizardSteps;
            steps.DataBind();
        }

        public string GetHeaderText()
        {
            return string.Format("Step {0}: {1}", Wizard1.ActiveStepIndex + 1, Wizard1.ActiveStep.Title);
        }

        public string GetItemStyle(object obj)
        {
            WizardStep step = obj as WizardStep;
            if (step == null)
            {
                return "";
            }

            int stepIndex = Wizard1.WizardSteps.IndexOf(step);
            if (stepIndex < Wizard1.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > Wizard1.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        private void LocalizeWizard()
        {
            Wizard1.WizardSteps[0].Title = Localize("Common.UserControls.ExcelImport.stepUpload", "Upload File");
            Wizard1.WizardSteps[1].Title = Localize("Common.UserControls.ExcelImport.stepPickup", "Pickup Sheet");
            Wizard1.WizardSteps[2].Title = Localize("Common.UserControls.ExcelImport.stepSetHeader", "Set Header Row");
            Wizard1.WizardSteps[3].Title = Localize("Common.UserControls.ExcelImport.stepMap", "Map Fields");
            Wizard1.WizardSteps[4].Title = Localize("Common.UserControls.ExcelImport.stepPreview", "Preview Data");
            Wizard1.WizardSteps[5].Title = Localize("Common.UserControls.ExcelImport.stepComplete", "Complete");

            Button btnStartNext = (Button)Wizard1.FindControl("StartNavigationTemplateContainerID$StartNextButton");
            btnStartNext.Text = Localize("Common.UserControls.ExcelImport.btnStartNext", "Next >>");

            Button btnStepNext = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepNextButton");
            btnStepNext.Text = Localize("Common.UserControls.ExcelImport.btnStepNext", "Next >>");

            Button btnStepPrevious = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
            btnStepPrevious.Text = Localize("Common.UserControls.ExcelImport.btnStepPrevious", "<< Previous");

            Button btnFinishPrevious = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishPreviousButton");
            btnFinishPrevious.Text = Localize("Common.UserControls.ExcelImport.btnFinishPrevious", "<< Previous");

            Button btnFinish = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishButton");
            btnFinish.Text = Localize("Common.UserControls.ExcelImport.btnFinish", "Submit");
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();

            LocalizeWizard();

            lblSelectFile.Text = Localize("Common.UserControls.ExcelImport.lblSelectFile", "Select the excel file: ");
            lblSelectSheet.Text = Localize("Common.UserControls.ExcelImport.lblSelectSheet", "Select the excel sheet: ");
            lblComplete.Text = Localize("Common.UserControls.ExcelImport.lblComplete", "Update completed.");
        }
    }
}