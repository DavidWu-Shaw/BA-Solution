using Framework.Data;
using CRM.Data;
using System.Collections.Generic;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface ISupplierRepository : IUpdateEntityRepository<SupplierData>
    {
        IEnumerable<SupplierInfoData> Search(SupplierCriteria criteria);
        SupplierInfoData RetrieveSupplierInfo(object supplierId);
    }
}
