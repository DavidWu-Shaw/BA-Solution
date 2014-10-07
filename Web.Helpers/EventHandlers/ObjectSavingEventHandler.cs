using System;
using Framework.Core;

namespace BA.Web.Common.Helper
{
    public delegate void ObjectSavingEventHandler(object sender, ObjectSavingEventArgs e);

    public class ObjectSavingEventArgs : EventArgs
    {
        public ObjectSavingEventArgs(object instance)
        {
            Instance = instance;
        }

        public bool IsSuccessful { get; set; }
        public object Instance { get; set; }
    }
}