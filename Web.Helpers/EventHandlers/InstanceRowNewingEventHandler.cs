using System;
using Framework.Core;

namespace BA.Web.Common.Helper
{
    public delegate void InstanceRowNewingEventHandler(object sender, InstanceRowNewingEventArgs e);

    public class InstanceRowNewingEventArgs : EventArgs
    {
        public InstanceRowNewingEventArgs(string instanceType)
        {
            InstanceType = instanceType;
        }

        public string InstanceType { get; set; }
        public BaseDto Instance { get; set; }
    }
}