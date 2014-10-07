using Framework.Data;

namespace SubjectEngine.Data
{
    /// <summary>
    /// Summary description for EntityValueData.
    /// </summary>
    public class DEntityItemData : ChildDataObject
    {
        public DEntityItemData()
        {
        }

        public virtual int Value { get; set; }
        public virtual string Text { get; set; }
    }
}
