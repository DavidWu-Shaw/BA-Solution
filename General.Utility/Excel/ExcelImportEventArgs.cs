using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Utility.Excel
{
    public delegate void ExcelImportEventHandler(object sender, ExcelImportEventArgs e);

    public class ExcelImportEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public ExcelSheet Result { get; set; }
    }

}
