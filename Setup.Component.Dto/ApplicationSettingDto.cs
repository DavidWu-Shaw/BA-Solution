using Framework.Core;

namespace Setup.Component.Dto
{
    public class ApplicationSettingDto : BaseDto
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
