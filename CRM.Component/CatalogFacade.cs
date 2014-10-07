using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class CatalogFacade : BusinessComponent
    {
        public CatalogFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            CatalogSystem = new CatalogSystem(unitOfWork);
        }

        private CatalogSystem CatalogSystem { get; set; }

        public IEnumerable<TDto> RetrieveAllCatalog<TDto>(IDataConverter<CatalogData, TDto> converter)
            where TDto : class
        {
            IEnumerable<TDto> instances = CatalogSystem.RetrieveAllCatalog(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewCatalog<TDto>(object id, IDataConverter<CatalogData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto instance = CatalogSystem.RetrieveOrNewCatalog(id, converter);
            UnitOfWork.CommitTransaction();
            return instance;
        }

        public IFacadeUpdateResult<CatalogData> SaveCatalog(CatalogDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<CatalogData> result = CatalogSystem.SaveCatalog(dto);
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

        public IFacadeUpdateResult<CatalogData> DeleteCatalog(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<CatalogData> result = CatalogSystem.DeleteCatalog(id);
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
    }
}
