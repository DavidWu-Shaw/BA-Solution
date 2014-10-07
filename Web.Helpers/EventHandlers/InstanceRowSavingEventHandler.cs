using System;
using Framework.Core;

namespace BA.Web.Common.Helper
{
    public delegate void InstanceRowSavingEventHandler(object sender, InstanceRowSavingEventArgs e);

    public class InstanceRowSavingEventArgs : EventArgs
    {
        public InstanceRowSavingEventArgs(BaseDto instance, string instanceType)
        {
            Instance = instance;
            InstanceType = instanceType;
        }

        public string InstanceType { get; set; }
        public bool IsSuccessful { get; set; }
        public BaseDto Instance { get; set; }
    }
}