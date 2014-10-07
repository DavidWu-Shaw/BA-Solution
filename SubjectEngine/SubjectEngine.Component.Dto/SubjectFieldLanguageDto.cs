using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class SubjectFieldLanguageDto : BaseDto
    {
        public object LanguageId { get; set; }
        public string FieldLabel { get; set; }
        public string HelpText { get; set; }
    }
}
