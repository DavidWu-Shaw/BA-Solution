using System.Collections.Generic;
using Framework.Data;
using CRM.Data;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface IOrderRepository : IUpdateEntityRepository<OrderData>
    {
        IEnumerable<OrderData> SearchBySupplier(object supplierId);
        IEnumerable<OrderData> SearchBySupplierAndStatus(object supplierId, int statusId);
        IEnumerable<OrderData> SearchByCustomer(object customerId);
        IEnumerable<OrderData> SearchByContact(object id);

        IEnumerable<OrderBriefInfoData> Search(OrderCriteria criteria);
        IEnumerable<OrderBriefInfoData> GetOrdersInProcess(OrderCriteria criteria);
        OrderInfoData GetOrderInfo(object orderId);
        OrderInfoData GetOrderInfo(string orderNumber);
        IEnumerable<OrderInfoData> GetOrdersInfo(IEnumerable<object> orderIds);
    }
}
