using Framework.Business;
using Framework.Validation;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
    public class DataType : BusinessObject<DataTypeData>
    {
        [RequiredField("DataTypeName", "The Name must be defined.")]
        public string Name
        {
            get
            {
                return Data.Name;
            }
            set
            {
                Data.Name = value;
            }
        }

        public string Description
        {
            get
            {
                return Data.Description;
            }
            set
            {
                Data.Description = value;
            }
        }

        public string DucType
        {
            get
            {
                return Data.DucType;
            }
            set
            {
                Data.DucType = value;
            }
        }
    }
}
