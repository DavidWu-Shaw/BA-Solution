using System;
using System.Collections.Generic;
using Framework.Core;

namespace BA.Web.Common.Helper
{
    public delegate void NeedListInstancesEventHandler(object sender, NeedListInstancesEventArgs e);

    public class NeedListInstancesEventArgs : EventArgs
    {
        public NeedListInstancesEventArgs(string instanceType)
        {
            InstanceType = instanceType;
        }

        public string InstanceType { get; set; }
        public IEnumerable<BaseDto> Instances { get; set; }
    }
}