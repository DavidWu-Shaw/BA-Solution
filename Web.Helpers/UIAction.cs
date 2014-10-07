using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Helpers
{
    public class UIAction
    {
        public event CommandEventHandler Executed;

        public string Caption
        {
            get;
            set;
        }

        public string Tooltip
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }
    }
}
