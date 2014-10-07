using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class LanguageSystem : BusinessComponent
    {
        public LanguageSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllLanguage<TDto>(IDataConverter<LanguageData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ILanguageService service = UnitOfWork.GetService<ILanguageService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return new List<TDto>();
        }

        internal TDto RetrieveOrNewLanguage<TDto>(object instanceId, IDataConverter<LanguageData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ILanguageService service = UnitOfWork.GetService<ILanguageService>();
            FacadeUpdateResult<LanguageData> result = new FacadeUpdateResult<LanguageData>();
            Language instance = RetrieveOrNew<LanguageData, Language, ILanguageService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<LanguageData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<LanguageData> SaveLanguage(LanguageDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<LanguageData> result = new FacadeUpdateResult<LanguageData>();
            ILanguageService service = UnitOfWork.GetService<ILanguageService>();
            Language instance = RetrieveOrNew<LanguageData, Language, ILanguageService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.Label = dto.Label;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<LanguageData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<LanguageData> DeleteLanguage(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<LanguageData> result = new FacadeUpdateResult<LanguageData>();
            ILanguageService service = UnitOfWork.GetService<ILanguageService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Language instance = query.ToBo<Language>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "LanguageCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ILanguageService service = UnitOfWork.GetService<ILanguageService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (LanguageData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
