using Framework.Business;
using Framework.Validation;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
    public class SubjectChildList : ChildBusinessObject<Subject, SubjectChildListData>
    {
        public object ChildListSubjectId
        {
            get { return Data.ChildListSubjectId; }
            set { Data.ChildListSubjectId = value; }
        }

        [RequiredField("SubjectChildListTitleRequired", "The Title must be defined.")]
        [StringLength("SubjectChildListTitleLength", "The Title must have a length less than {1}", MaxLength = 100)]
        public string Title
        {
            get { return Data.Title; }
            set { Data.Title = value; }
        }

        public bool AllowAdd
        {
            get { return Data.AllowAdd; }
            set { Data.AllowAdd = value; }
        }

        public bool AllowEdit
        {
            get { return Data.AllowEdit; }
            set { Data.AllowEdit = value; }
        }

        public bool AllowDelete
        {
            get { return Data.AllowDelete; }
            set { Data.AllowDelete = value; }
        }

        public int Sort
        {
            get { return Data.Sort; }
            set { Data.Sort = value; }
        }

        public bool AllowFiltering
        {
            get { return Data.AllowFiltering; }
            set { Data.AllowFiltering = value; }
        }
        public bool AllowImport
        {
            get { return Data.AllowImport; }
            set { Data.AllowImport = value; }
        }
        public string ImportUrl
        {
            get { return Data.ImportUrl; }
            set { Data.ImportUrl = value; }
        }
    }
}
