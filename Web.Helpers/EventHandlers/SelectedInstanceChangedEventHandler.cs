using System;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public delegate void SelectedInstanceChangedEventHandler(object sender, SelectedInstanceChangedEventArgs e);

    public class SelectedInstanceChangedEventArgs : EventArgs
    {
        public SelectedInstanceChangedEventArgs(object instanceId)
        {
            InstanceId = instanceId;
        }
        public object InstanceId { get; set; }
    }
}