using Framework.Data;

namespace SubjectEngine.Data
{
    public class DataTypeData : DataObject
    {
        public DataTypeData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string DucType { get; set; }
    }
}
