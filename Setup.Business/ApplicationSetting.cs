using Framework.Business;
using Setup.Data;
using Framework.Validation;

namespace Setup.Business
{
    public class ApplicationSetting : BusinessObject<ApplicationSettingData>
    {
        [RequiredField("ApplicationSettingSettingKeyRequired", "The SettingKey must be defined.")]
        [StringLength("ApplicationSettingSettingKeyLength", "The SettingKey must have a length less than {1}", MaxLength = 50)]
        public string SettingKey
        {
            get
            {
                return Data.SettingKey;
            }
            set
            {
                Data.SettingKey = value;
            }
        }

        [StringLength("ApplicationSettingSettingValueLength", "The SettingValue must have a length less than {1}", MaxLength = 100)]
        public string SettingValue
        {
            get
            {
                return Data.SettingValue;
            }
            set
            {
                Data.SettingValue = value;
            }
        }
    }
}
