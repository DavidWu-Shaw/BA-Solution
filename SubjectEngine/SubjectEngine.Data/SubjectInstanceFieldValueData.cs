using System;
using Framework.Data;

namespace SubjectEngine.Data
{
    public class SubjectInstanceFieldValueData : ChildDataObject
    {
        public SubjectInstanceFieldValueData()
        {
        }

        public virtual object SubjectFieldId { get; set; }
        public virtual int? ValueInt { get; set; }
        public virtual string ValueText { get; set; }
        public virtual DateTime? ValueDate { get; set; }
    }
}
