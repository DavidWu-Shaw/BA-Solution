using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;
using System.Collections.Generic;

namespace CRM.Service
{
    public class SupplierService : UpdateEntityService<ISupplierRepository, SupplierData>, ISupplierService
    {
        public IServiceQueryResultList<SupplierInfoData> Search(SupplierCriteria criteria)
        {
            IEnumerable<SupplierInfoData> result = Repository.Search(criteria);
            return ServiceResultFactory.BuildServiceQueryResult<SupplierInfoData>(result);
        }

        public IServiceQueryResult<SupplierInfoData> RetrieveSupplierInfo(object supplierId)
        {
            SupplierInfoData result = Repository.RetrieveSupplierInfo(supplierId);
            return ServiceResultFactory.BuildServiceQueryResult<SupplierInfoData>(result);
        }
    }
}
