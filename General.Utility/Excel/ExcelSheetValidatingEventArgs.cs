using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Utility.Excel
{
    public delegate void ExcelSheetValidatingEventHandler(object sender, ExcelSheetValidatingEventArgs e);

    public class ExcelSheetValidatingEventArgs : EventArgs
    {
        public ExcelSheetValidatingEventArgs(ExcelSheet sheet)
        {
            Sheet = sheet;
        }

        public bool IsCancelled { get; set; }
        public ExcelSheet Sheet { get; private set; }
    }

}
