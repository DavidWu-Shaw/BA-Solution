using Framework.Business;
using Setup.Data;
using Framework.Validation;
using System;

namespace Setup.Business
{
    public class LanguagePhrase : BusinessObject<LanguagePhraseData>
    {
        [RequiredField("LanguagePhraseLanguageIdRequired", "The LanguageId must be defined.")]
        public object LanguageId
        {
            get { return Data.LanguageId; }
            set { Data.LanguageId = value; }
        }

        [RequiredField("LanguagePhrasePhraseKeyRequired", "The PhraseKey must be defined.")]
        [StringLength("LanguagePhrasePhraseKeyLength", "The PhraseKey must have a length less than {1}", MaxLength = 200)]
        public string PhraseKey
        {
            get { return Data.PhraseKey; }
            set { Data.PhraseKey = value; }
        }

        [StringLength("LanguagePhrasePhraseValueLength", "The PhraseValue must have a length less than {1}", MaxLength = 500)]
        public string PhraseValue
        {
            get { return Data.PhraseValue; }
            set { Data.PhraseValue = value; }
        }

        public DateTime? DateModified
        {
            get { return Data.DateModified; }
            set { Data.DateModified = value; }
        }
    }
}
