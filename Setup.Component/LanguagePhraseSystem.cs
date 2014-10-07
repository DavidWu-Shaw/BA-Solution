using System;
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
    internal class LanguagePhraseSystem : BusinessComponent
    {
        public LanguagePhraseSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveLanguagePhrasesByLanguage<TDto>(object languageId, IDataConverter<LanguagePhraseData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("languageId", languageId);
            ArgumentValidator.IsNotNull("converter", converter);

            ILanguagePhraseService service = UnitOfWork.GetService<ILanguagePhraseService>();
            var query = service.SearchByLanguage(languageId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewLanguagePhrase<TDto>(object instanceId, IDataConverter<LanguagePhraseData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ILanguagePhraseService service = UnitOfWork.GetService<ILanguagePhraseService>();
            FacadeUpdateResult<LanguagePhraseData> result = new FacadeUpdateResult<LanguagePhraseData>();
            LanguagePhrase instance = RetrieveOrNew<LanguagePhraseData, LanguagePhrase, ILanguagePhraseService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<LanguagePhraseData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<LanguagePhraseData> SaveLanguagePhrase(LanguagePhraseDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<LanguagePhraseData> result = new FacadeUpdateResult<LanguagePhraseData>();
            ILanguagePhraseService service = UnitOfWork.GetService<ILanguagePhraseService>();
            LanguagePhrase instance = RetrieveOrNew<LanguagePhraseData, LanguagePhrase, ILanguagePhraseService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.LanguageId = dto.LanguageId;
                instance.PhraseKey = dto.PhraseKey;
                instance.PhraseValue = dto.PhraseValue;
                instance.DateModified = DateTime.Now;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<LanguagePhraseData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<LanguagePhraseData> DeleteLanguagePhrase(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<LanguagePhraseData> result = new FacadeUpdateResult<LanguagePhraseData>();
            ILanguagePhraseService service = UnitOfWork.GetService<ILanguagePhraseService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                LanguagePhrase instance = query.ToBo<LanguagePhrase>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "LanguagePhraseCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ILanguagePhraseService service = UnitOfWork.GetService<ILanguagePhraseService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (LanguagePhraseData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.PhraseKey));
                }
            }

            return dataSource;
        }
    }
}
