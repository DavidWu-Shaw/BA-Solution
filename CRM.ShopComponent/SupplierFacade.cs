using System.Collections.Generic;
using Framework.Component;
using CRM.Data;
using CRM.Data.Criterias;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class SupplierFacade : BusinessComponent
    {
        public SupplierFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            SupplierSystem = new SupplierSystem(unitOfWork);
        }

        private SupplierSystem SupplierSystem { get; set; }

        public List<TDto> RetrieveSuppliersByCategory<TDto>(object categoryId, IDataConverter<SupplierInfoData, TDto> converter)
            where TDto : class
        {
            SupplierCriteria criteria = new SupplierCriteria();
            criteria.CategoryId = categoryId;
            UnitOfWork.BeginTransaction();
            List<TDto> instances = SupplierSystem.RetrieveSuppliersBySearch(criteria, converter);
            UnitOfWork.CommitTransaction();
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveSuppliersBySearch<TDto>(SupplierCriteria criteria, IDataConverter<SupplierInfoData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = SupplierSystem.RetrieveSuppliersBySearch(criteria, converter);
            UnitOfWork.CommitTransaction();
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveAllSupplier<TDto>(IDataConverter<SupplierInfoData, TDto> converter)
            where TDto : class
        {
            SupplierCriteria criteria = new SupplierCriteria();
            return RetrieveSuppliersBySearch(criteria, converter);
        }

        public TDto RetrieveSupplierInfo<TDto>(object supplierId, IDataConverter<SupplierInfoData, TDto> converter)
            where TDto : class
        {
            TDto instance = SupplierSystem.RetrieveSupplierInfo(supplierId, converter);
            return instance;
        }
    }
}
