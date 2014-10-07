using Framework.Data;

namespace Setup.Data
{
    public class DataPhraseData : DataObject
    {
        public virtual object LanguageId { get; set; }
        public virtual string PhraseKey { get; set; }
        public virtual string PhraseValue { get; set; }
    }
}
