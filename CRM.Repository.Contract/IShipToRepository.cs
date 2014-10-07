using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IShipToRepository : IUpdateEntityRepository<ShipToData>
    {
        IEnumerable<ShipToData> SearchByCustomer(object customerId);
        IEnumerable<ShipToData> SearchByContactPhone(string phone);
    }
}
