using System;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public delegate void InstanceChangedEventHandler(object sender, InstanceChangedEventArgs e);

    public class InstanceChangedEventArgs : EventArgs
    {
        public InstanceChangedEventArgs(object instance)
        {
            Instance = instance;
        }
        public object Instance { get; set; }
    }
}