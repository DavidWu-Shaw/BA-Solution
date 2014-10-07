using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Utility.Excel
{
    public delegate void ExcelSheetRowValidatingEventHandler(object sender, ExcelSheetRowValidatingEventArgs e);

    public class ExcelSheetRowValidatingEventArgs : EventArgs
    {
        public ExcelSheetRowValidatingEventArgs(ExcelSheetRow row)
        {
            Row = row;
        }

        public bool IsCancelled { get; set; }
        public ExcelSheetRow Row { get; private set; }
    }
}
