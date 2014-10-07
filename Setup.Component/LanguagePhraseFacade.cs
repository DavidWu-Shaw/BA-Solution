using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Data;

namespace Setup.Component
{
    public class LanguagePhraseFacade : BusinessComponent
    {
        public LanguagePhraseFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            LanguagePhraseSystem = new LanguagePhraseSystem(unitOfWork);
        }

        private LanguagePhraseSystem LanguagePhraseSystem { get; set; }

        public List<LanguagePhraseDto> RetrieveLanguagePhrasesByLanguage(object id)
        {
            UnitOfWork.BeginTransaction();
            List<LanguagePhraseDto> instances = LanguagePhraseSystem.RetrieveLanguagePhrasesByLanguage(id, new LanguagePhraseConverter());
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<LanguagePhraseDto>();
            }
            return instances;
        }

        public LanguagePhraseDto RetrieveOrNewLanguagePhrase(object id)
        {
            return LanguagePhraseSystem.RetrieveOrNewLanguagePhrase(id, new LanguagePhraseConverter());
        }

        public IFacadeUpdateResult<LanguagePhraseData> SaveLanguagePhrase(LanguagePhraseDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<LanguagePhraseData> result = LanguagePhraseSystem.SaveLanguagePhrase(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<LanguagePhraseData> DeleteLanguagePhrase(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<LanguagePhraseData> result = LanguagePhraseSystem.DeleteLanguagePhrase(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IList<BindingListItem> GetBindingList()
        {
            return LanguagePhraseSystem.GetBindingList();
        }

    }
}
