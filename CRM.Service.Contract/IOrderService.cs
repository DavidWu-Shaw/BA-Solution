using CRM.Data;
using Framework.Service;
using Framework.UoW;
using System.Collections.Generic;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface IOrderService : IUpdateEntityService<OrderData>
    {
        IServiceQueryResultList<OrderData> SearchBySupplier(object supplierId);
        IServiceQueryResultList<OrderData> SearchBySupplierAndStatus(object supplierId, int statusId);
        IServiceQueryResultList<OrderData> SearchByCustomer(object customerId);
        IServiceQueryResultList<OrderData> SearchByContact(object id);
        // For ShopComponent
        IServiceQueryResultList<OrderBriefInfoData> Search(OrderCriteria criteria);
        IServiceQueryResultList<OrderBriefInfoData> GetOrdersInProcess(OrderCriteria criteria);
        IServiceQueryResult<OrderInfoData> GetOrderInfo(object orderId);
        IServiceQueryResult<OrderInfoData> GetOrderInfo(string orderNumber);
        IServiceQueryResultList<OrderInfoData> GetOrdersInfo(IEnumerable<object> orderIds);
    }
}
