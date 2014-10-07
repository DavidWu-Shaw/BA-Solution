using System;
using Framework.Core;

namespace Setup.Component.Dto
{
    public class LanguagePhraseDto : BaseDto
    {
        public object LanguageId { get; set; }
        public string PhraseKey { get; set; }
        public string PhraseValue { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
