using System;
using System.Text;
using General.Utility.DataValidate;

namespace General.Utility.Excel
{
    [Serializable]
    public class ExcelSheetCell
    {
        public string Value { get; set; }
        public ExcelSheetColumn Column { get; set; }
        public DataValidateResult ValidateResult { get; set; }

        public ExcelSheetCell(ExcelSheetColumn column)
        {
            Column = column;
            Value = string.Empty;
            ValidateResult = new DataValidateResult();
        }

        public bool IsValid
        {
            get
            {
                return ValidateResult.IsValid;
            }
        }

        public string ValidateResultString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationItem item in ValidateResult.Items)
                {
                    sb.AppendFormat("{0}: {1}", Column.Name, item.Message);
                }

                return sb.ToString();
            }
        }

        public void Validate()
        {
            ValidateResult.Clear();

            if (Column.Validator != null)
            {
                DataValidateResult validateResult = Column.Validator.Validate(Value);
                if (!validateResult.IsValid)
                {
                    ValidateResult.Merge(validateResult);
                }
            }
        }
    }
}