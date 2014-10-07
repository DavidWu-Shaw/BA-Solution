using System;
using System.Collections.Generic;
using System.Data;

namespace General.Utility.Excel
{
    [Serializable]
    public class ExcelSheet
    {
        public string Name { get; set; }
        public IList<ExcelSheetRow> Rows { get; set; }
        public ExcelSheetColumnCollection Columns { get; set; }
        public int DataStartRowIndex { get; set; }

        public ExcelSheet()
            : this(string.Empty)
        {
        }

        public ExcelSheet(string name)
        {
            Name = name;
            Rows = new List<ExcelSheetRow>();
            Columns = new ExcelSheetColumnCollection();
        }

        public bool IsValid
        {
            get
            {
                return InvalidRowCount == 0;
            }
        }

        public int InvalidRowCount
        {
            get
            {
                int invalidRowCount = 0;
                foreach (ExcelSheetRow row in Rows)
                {
                    if (!row.IsValid)
                    {
                        invalidRowCount++;
                    }
                }

                return invalidRowCount;
            }
        }

        // Creates a new ExcelSheetRow with the same schema as the ExcelSheet.
        public ExcelSheetRow NewRow()
        {
            ExcelSheetRow row = new ExcelSheetRow();
            row.Sheet = this;
            foreach (ExcelSheetColumn column in Columns)
            {
                ExcelSheetCell cell = new ExcelSheetCell(column);
                row.Cells.Add(cell);
            }

            return row;
        }

        public ExcelSheetRow this[int rowIndex]
        {
            get
            {
                return Rows[rowIndex];
            }
        }

        public void SetHeaderByRow(int rowIndex)
        {
            if (rowIndex < Rows.Count)
            {
                DataStartRowIndex = rowIndex + 1;

                ExcelSheetRow headerRow = Rows[rowIndex];
                int colIndex = 0;
                foreach (ExcelSheetColumn column in this.Columns)
                {
                    string value = headerRow[colIndex].Value;
                    if (string.IsNullOrEmpty(value))
                    {
                        column.Name = string.Format("F{0}", colIndex + 1);
                    }
                    else
                    {
                        column.Name = headerRow[colIndex].Value;
                    }

                    colIndex++;
                }
            }
        }

        public void ResetMapping()
        {
            int index = 0;
            foreach (ExcelSheetColumn column in Columns)
            {
                column.TargetName = null;
                index++;
            }
            foreach (ExcelSheetRow row in Rows)
            {
                row.RowLevelValidateResult.Clear();
            }
        }

        public void ResetHeader()
        {
            int index = 0;
            foreach (ExcelSheetColumn column in Columns)
            {
                column.Name = string.Format("F{0}", index + 1);
                index++;
            }
        }

        public DataTable GetSampleData()
        {
            return GetSampleData(int.MaxValue);
        }

        public DataTable GetSampleData(int rowIndex)
        {
            DataTable table = new DataTable(Name);
            foreach (ExcelSheetColumn column in this.Columns)
            {
                table.Columns.Add(column.Name);
            }

            int index = 0;
            foreach (ExcelSheetRow row in this.Rows)
            {
                DataRow dataRow = table.NewRow();
                table.Rows.Add(dataRow);

                int colIndex = 0;
                foreach (ExcelSheetColumn column in this.Columns)
                {
                    dataRow[column.Name] = row[colIndex].Value;
                    colIndex++;
                }

                index++;
                if (index >= rowIndex)
                {
                    break;
                }
            }

            return table;
        }
    }
}