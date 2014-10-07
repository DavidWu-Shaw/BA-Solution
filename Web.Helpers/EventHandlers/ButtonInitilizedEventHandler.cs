using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Web.Helpers.EventHandlers
{
    public delegate void ButtonInitilizedEventHandler(object sender, ButtonInitilizedEventArgs e);

    public class ButtonInitilizedEventArgs : EventArgs
    {
        public ButtonInitilizedEventArgs(Button button)
        {
            ButtonControl = button;
        }
        public Button ButtonControl { get; set; }
    }
}
