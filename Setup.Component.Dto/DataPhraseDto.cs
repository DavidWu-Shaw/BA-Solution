using Framework.Core;

namespace Setup.Component.Dto
{
    public class DataPhraseDto : BaseDto
    {
        public object LanguageId { get; set; }
        public string PhraseKey { get; set; }
        public string PhraseValue { get; set; }
    }
}
