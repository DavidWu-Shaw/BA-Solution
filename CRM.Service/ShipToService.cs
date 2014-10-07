using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class ShipToService : UpdateEntityService<IShipToRepository, ShipToData>, IShipToService
    {
        public IServiceQueryResultList<ShipToData> SearchByCustomer(object customerId)
        {
            IEnumerable<ShipToData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<ShipToData>(result);
        }

        public IServiceQueryResultList<ShipToData> SearchByContactPhone(string phone)
        {
            IEnumerable<ShipToData> result = Repository.SearchByContactPhone(phone);
            return ServiceResultFactory.BuildServiceQueryResult<ShipToData>(result);
        }
    }
}
