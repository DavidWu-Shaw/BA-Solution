using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class CatalogSystem : BusinessComponent
    {
        public CatalogSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllCatalog<TDto>(IDataConverter<CatalogData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ICatalogService service = UnitOfWork.GetService<ICatalogService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewCatalog<TDto>(object instanceId, IDataConverter<CatalogData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ICatalogService service = UnitOfWork.GetService<ICatalogService>();
            FacadeUpdateResult<CatalogData> result = new FacadeUpdateResult<CatalogData>();
            Catalog instance = RetrieveOrNew<CatalogData, Catalog, ICatalogService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<CatalogData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<CatalogData> SaveCatalog(CatalogDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<CatalogData> result = new FacadeUpdateResult<CatalogData>();
            ICatalogService service = UnitOfWork.GetService<ICatalogService>();
            Catalog instance = RetrieveOrNew<CatalogData, Catalog, ICatalogService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.Description = dto.Description;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<CatalogData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<CatalogData> DeleteCatalog(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<CatalogData> result = new FacadeUpdateResult<CatalogData>();
            ICatalogService service = UnitOfWork.GetService<ICatalogService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Catalog instance = query.ToBo<Catalog>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "CatalogCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            // Only get Catalogs which are not Global
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ICatalogService service = UnitOfWork.GetService<ICatalogService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (CatalogData data in query.DataList.Where(o => o.IsGlobal != true))
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
