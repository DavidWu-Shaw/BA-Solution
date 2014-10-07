using System;
using System.Collections.Generic;
using System.Text;
using General.Utility.DataValidate;

namespace General.Utility.Excel
{
    [Serializable]
    public class ExcelSheetRow
    {
        public IList<ExcelSheetCell> Cells { get; set; }
        public ExcelSheet Sheet { get; set; }
        public int RowNo { get; set; }
        // Store customized data
        public object Tag { get; set; }
        // SheetRow level invalid info, 
        public DataValidateResult RowLevelValidateResult { get; set; }

        public ExcelSheetRow()
        {
            Cells = new List<ExcelSheetCell>();
            RowLevelValidateResult = new DataValidateResult();
        }

        public bool IsValid 
        { 
            get
            {
                if (!RowLevelValidateResult.IsValid)
                {
                    return false;
                }
                else
                {
                    bool isValid = true;

                    foreach (ExcelSheetCell cell in Cells)
                    {
                        if (!cell.IsValid)
                        {
                            isValid = false;
                            break;
                        }
                    }

                    return isValid;
                }
            }
        }

        public string ValidateResultString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationItem item in RowLevelValidateResult.Items)
                {
                    sb.AppendFormat("{0};", item.Message);
                }
                foreach (ExcelSheetCell cell in Cells)
                {
                    if (!cell.IsValid)
                    {
                        sb.AppendFormat("{0};", cell.ValidateResultString);
                    }
                }

                return sb.ToString();

            }
        }

        public ExcelSheetCell this[int columnIndex]
        {
            get
            {
                return Cells[columnIndex];
            }
        }

        public ExcelSheetCell this[string columnName]
        {
            get
            {
                int index = Sheet.Columns.IndexOf(columnName);
                if (index >= 0)
                {
                    return Cells[index];
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetValue(string targetColumn)
        {
            int index = Sheet.Columns.GetIndexByTargetColumn(targetColumn);
            if (index >= 0)
            {
                return Cells[index].Value;
            }
            else
            {
                return null;
            }
        }
    }
}