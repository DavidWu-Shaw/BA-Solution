using System;
using General.Utility.DataValidate;

namespace General.Utility.Excel
{
    [Serializable]
    public class ExcelSheetColumn
    {
        // Requested column name
        public string TargetName { get; set; }
        // Column name from Excel file
        public string Name { get; set; }
        public IDataValidator Validator { get; set; }

        public bool IsMapped
        {
            get
            {
                return !string.IsNullOrEmpty(TargetName);
            }
        }

        public ExcelSheetColumn(string columnName)
        {
            InitColumn(columnName, null);
        }

        public ExcelSheetColumn(string columnName, IDataValidator validator)
        {
            InitColumn(columnName, validator);
        }

        private void InitColumn(string columnName, IDataValidator validator)
        {
            Name = columnName;
            Validator = validator;
        }
    }
}