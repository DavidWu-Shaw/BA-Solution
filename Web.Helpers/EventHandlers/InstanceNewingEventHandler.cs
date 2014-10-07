using System;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public delegate void InstanceNewingEventHandler(object sender, InstanceNewingEventArgs e);

    public class InstanceNewingEventArgs : EventArgs
    {
        public InstanceNewingEventArgs(string instanceType)
        {
            InstanceType = instanceType;
        }

        public string InstanceType { get; set; }
    }
}