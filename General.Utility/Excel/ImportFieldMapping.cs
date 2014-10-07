using System;
using General.Utility.DataValidate;

namespace General.Utility.Excel
{
    [Serializable]
    public class ImportFieldMapping
    {
        // Requested field name from caller page
        public string RequestField { get; set; }
        public bool IsMust { get; set; }
        // Source column name from Excel file
        public string ColumnName { get; set; }
        public IDataValidator Validator { get; set; }

        public ImportFieldMapping(string requestField, bool isMust)
        {
            RequestField = requestField;
            IsMust = isMust;
            ColumnName = null;
        }

        public ImportFieldMapping(string requestField)
            : this(requestField, false)
        {
        }

        public string RequestFieldDisplay
        {
            get
            {
                if (IsMust)
                {
                    return RequestField + "*";
                }
                else
                {
                    return RequestField;
                }
            }
        }
    }
}