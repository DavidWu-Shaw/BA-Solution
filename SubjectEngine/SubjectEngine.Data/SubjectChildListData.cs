using System.Collections.Generic;
using Framework.Data;

namespace SubjectEngine.Data
{
    public class SubjectChildListData : ChildDataObject
    {
        public SubjectChildListData()
        {
            SubjectChildListLanguagesData = new List<SubjectChildListLanguageData>();
        }

        public virtual object ChildListSubjectId { get; set; }
        public virtual string Title { get; set; }
        public virtual bool AllowAdd { get; set; }
        public virtual bool AllowEdit { get; set; }
        public virtual bool AllowDelete { get; set; }
        public virtual int Sort { get; set; }
        public virtual bool AllowFiltering { get; set; }
        public virtual string ImportUrl { get; set; }
        public virtual bool AllowImport { get; set; }

        public virtual IList<SubjectChildListLanguageData> SubjectChildListLanguagesData { get; set; }
    }
}
