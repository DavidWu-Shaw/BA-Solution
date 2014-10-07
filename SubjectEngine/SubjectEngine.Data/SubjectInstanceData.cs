using System;
using System.Collections.Generic;
using Framework.Data;

namespace SubjectEngine.Data
{
    public class SubjectInstanceData : ChildDataObject
    {
        public SubjectInstanceData()
        {
            SubjectInstanceFieldValuesData = new List<SubjectInstanceFieldValueData>();
        }

        public virtual object InstanceId { get; set; }
        public virtual DateTime InstanceCreatedDate { get; set; }

        public virtual IList<SubjectInstanceFieldValueData> SubjectInstanceFieldValuesData { get; set; }
    }
}
