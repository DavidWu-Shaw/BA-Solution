using CRM.Data;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface ISupplierService : IUpdateEntityService<SupplierData>
    {
        IServiceQueryResultList<SupplierInfoData> Search(SupplierCriteria criteria);
        IServiceQueryResult<SupplierInfoData> RetrieveSupplierInfo(object supplierId);
    }
}
