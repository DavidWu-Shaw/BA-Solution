using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class DataPhraseSystem : BusinessComponent
    {
        public DataPhraseSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveDataPhrasesByLanguage<TDto>(object languageId, IDataConverter<DataPhraseData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("languageId", languageId);
            ArgumentValidator.IsNotNull("converter", converter);

            IDataPhraseService service = UnitOfWork.GetService<IDataPhraseService>();
            var query = service.SearchByLanguage(languageId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewDataPhrase<TDto>(object instanceId, IDataConverter<DataPhraseData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IDataPhraseService service = UnitOfWork.GetService<IDataPhraseService>();
            FacadeUpdateResult<DataPhraseData> result = new FacadeUpdateResult<DataPhraseData>();
            DataPhrase instance = RetrieveOrNew<DataPhraseData, DataPhrase, IDataPhraseService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<DataPhraseData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<DataPhraseData> SaveDataPhrase(DataPhraseDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<DataPhraseData> result = new FacadeUpdateResult<DataPhraseData>();
            IDataPhraseService service = UnitOfWork.GetService<IDataPhraseService>();
            DataPhrase instance = RetrieveOrNew<DataPhraseData, DataPhrase, IDataPhraseService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.LanguageId = dto.LanguageId;
                instance.PhraseKey = dto.PhraseKey;
                instance.PhraseValue = dto.PhraseValue;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<DataPhraseData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<DataPhraseData> DeleteDataPhrase(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<DataPhraseData> result = new FacadeUpdateResult<DataPhraseData>();
            IDataPhraseService service = UnitOfWork.GetService<IDataPhraseService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                DataPhrase instance = query.ToBo<DataPhrase>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "DataPhraseCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IDataPhraseService service = UnitOfWork.GetService<IDataPhraseService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (DataPhraseData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.PhraseKey));
                }
            }

            return dataSource;
        }
    }
}
