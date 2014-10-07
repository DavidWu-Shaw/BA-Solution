using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface IShipToService : IUpdateEntityService<ShipToData>
    {
        IServiceQueryResultList<ShipToData> SearchByCustomer(object customerId);
        IServiceQueryResultList<ShipToData> SearchByContactPhone(string phone);
    }
}
