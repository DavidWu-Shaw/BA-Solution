using Framework.Data;

namespace Setup.Data
{
    public class ApplicationSettingData : DataObject
    {
        public ApplicationSettingData()
        {
        }

        public virtual string SettingKey { get; set; }
        public virtual string SettingValue { get; set; }
    }
}
