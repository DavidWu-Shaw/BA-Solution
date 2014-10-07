using Framework.Data;

namespace SubjectEngine.Data
{
    public class SubjectLayoutData : ChildDataObject
    {
        public SubjectLayoutData()
        {
        }

        public virtual int ItemTypeId { get; set; }
        public virtual object SubjectFieldId { get; set; }
        public virtual int? SubjectActionId { get; set; }
        public virtual string SectionLabel { get; set; }
        public virtual int? RowIndex { get; set; }
        public virtual int? CellIndex { get; set; }
    }
}
