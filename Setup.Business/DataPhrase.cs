using Framework.Business;
using Setup.Data;
using Framework.Validation;

namespace Setup.Business
{
    public class DataPhrase : BusinessObject<DataPhraseData>
    {
        [RequiredField("DataPhraseLanguageIdRequired", "The LanguageId must be defined.")]
        public object LanguageId
        {
            get { return Data.LanguageId; }
            set { Data.LanguageId = value; }
        }

        [RequiredField("DataPhrasePhraseKeyRequired", "The PhraseKey must be defined.")]
        [StringLength("DataPhrasePhraseKeyLength", "The PhraseKey must have a length less than {1}", MaxLength = 200)]
        public string PhraseKey
        {
            get { return Data.PhraseKey; }
            set { Data.PhraseKey = value; }
        }

        [StringLength("DataPhrasePhraseValueLength", "The PhraseValue must have a length less than {1}", MaxLength = 500)]
        public string PhraseValue
        {
            get { return Data.PhraseValue; }
            set { Data.PhraseValue = value; }
        }
    }
}
