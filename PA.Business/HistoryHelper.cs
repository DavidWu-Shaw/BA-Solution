using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PA.Business
{
    public static class HistoryHelper
    {
        public static string ComposeHistoryContent(string fieldName, string newValue)
        {
            return string.Format("[{0}] changed to \"{1}\"", fieldName, newValue);
        }

        public static string ComposeHistoryContent(string fieldName)
        {
            return string.Format("[{0}] changed to NULL", fieldName);
        }
    }
}
