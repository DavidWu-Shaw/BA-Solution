using System;
using Framework.Core;

namespace BA.Web.Common.Helper
{
    public delegate void InstanceSavingEventHandler(object sender, InstanceSavingEventArgs e);

    public class InstanceSavingEventArgs : EventArgs
    {
        public InstanceSavingEventArgs(BaseDto instance)
        {
            Instance = instance;
        }

        public bool IsSuccessful { get; set; }
        public BaseDto Instance { get; private set; }
    }

}