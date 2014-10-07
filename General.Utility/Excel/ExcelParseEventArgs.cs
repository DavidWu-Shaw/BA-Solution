using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Utility.Excel
{
    public delegate void ExcelParseEventHandler(object sender, ExcelParseEventArgs e);

    public class ExcelParseEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

}
