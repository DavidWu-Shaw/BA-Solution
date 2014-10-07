using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Data;

namespace Setup.Component
{
    public class DataPhraseFacade : BusinessComponent
    {
        public DataPhraseFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            DataPhraseSystem = new DataPhraseSystem(unitOfWork);
        }

        private DataPhraseSystem DataPhraseSystem { get; set; }

        public List<DataPhraseDto> RetrieveDataPhrasesByLanguage(object id)
        {
            UnitOfWork.BeginTransaction();
            List<DataPhraseDto> instances = DataPhraseSystem.RetrieveDataPhrasesByLanguage(id, new DataPhraseConverter());
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<DataPhraseDto>();
            }
            return instances;
        }

        public DataPhraseDto RetrieveOrNewDataPhrase(object id)
        {
            return DataPhraseSystem.RetrieveOrNewDataPhrase(id, new DataPhraseConverter());
        }

        public IFacadeUpdateResult<DataPhraseData> SaveDataPhrase(DataPhraseDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DataPhraseData> result = DataPhraseSystem.SaveDataPhrase(dto);
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

        public IFacadeUpdateResult<DataPhraseData> DeleteDataPhrase(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DataPhraseData> result = DataPhraseSystem.DeleteDataPhrase(id);
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
            return DataPhraseSystem.GetBindingList();
        }

    }
}
