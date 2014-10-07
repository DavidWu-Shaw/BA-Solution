using Framework.Data;
using System;

namespace Setup.Data
{
    public class LanguagePhraseData : DataObject
    {
        public virtual object LanguageId { get; set; }
        public virtual string PhraseKey { get; set; }
        public virtual string PhraseValue { get; set; }
        public virtual DateTime? DateModified { get; set; }
    }
}
