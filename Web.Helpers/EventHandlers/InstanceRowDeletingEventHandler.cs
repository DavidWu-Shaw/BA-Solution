using System;
using Framework.Core;

namespace BA.Web.Common.Helper
{
    public delegate void InstanceRowDeletingEventHandler(object sender, InstanceRowDeletingEventArgs e);

    public class InstanceRowDeletingEventArgs : EventArgs
    {
        public InstanceRowDeletingEventArgs(BaseDto instance, string instanceType)
        {
            Instance = instance;
            InstanceType = instanceType;
        }
        public string InstanceType { get; set; }
        public BaseDto Instance { get; set; }
        public bool IsSuccessful { get; set; }
    }
}