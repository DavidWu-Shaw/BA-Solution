using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class OrderService : UpdateEntityService<IOrderRepository, OrderData>, IOrderService
    {
        public IServiceQueryResultList<OrderData> SearchBySupplier(object supplierId)
        {
            IEnumerable<OrderData> result = Repository.SearchBySupplier(supplierId);
            return ServiceResultFactory.BuildServiceQueryResult<OrderData>(result);
        }

        public IServiceQueryResultList<OrderData> SearchBySupplierAndStatus(object supplierId, int statusId)
        {
            IEnumerable<OrderData> result = Repository.SearchBySupplierAndStatus(supplierId, statusId);
            return ServiceResultFactory.BuildServiceQueryResult<OrderData>(result);
        }

        public IServiceQueryResultList<OrderData> SearchByCustomer(object customerId)
        {
            IEnumerable<OrderData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<OrderData>(result);
        }

        public IServiceQueryResultList<OrderData> SearchByContact(object contactId)
        {
            IEnumerable<OrderData> result = Repository.SearchByContact(contactId);
            return ServiceResultFactory.BuildServiceQueryResult<OrderData>(result);
        }

        public IServiceQueryResultList<OrderBriefInfoData> Search(OrderCriteria criteria)
        {
            IEnumerable<OrderBriefInfoData> result = Repository.Search(criteria);
            return ServiceResultFactory.BuildServiceQueryResult<OrderBriefInfoData>(result);
        }

        public IServiceQueryResultList<OrderBriefInfoData> GetOrdersInProcess(OrderCriteria criteria)
        {
            IEnumerable<OrderBriefInfoData> result = Repository.GetOrdersInProcess(criteria);
            return ServiceResultFactory.BuildServiceQueryResult<OrderBriefInfoData>(result);
        }

        public IServiceQueryResult<OrderInfoData> GetOrderInfo(object orderId)
        {
            OrderInfoData result = Repository.GetOrderInfo(orderId);
            return ServiceResultFactory.BuildServiceQueryResult<OrderInfoData>(result);
        }

        public IServiceQueryResult<OrderInfoData> GetOrderInfo(string orderNumber)
        {
            OrderInfoData result = Repository.GetOrderInfo(orderNumber);
            return ServiceResultFactory.BuildServiceQueryResult<OrderInfoData>(result);
        }

        public IServiceQueryResultList<OrderInfoData> GetOrdersInfo(IEnumerable<object> orderIds)
        {
            IEnumerable<OrderInfoData> result = Repository.GetOrdersInfo(orderIds);
            return ServiceResultFactory.BuildServiceQueryResult<OrderInfoData>(result);
        }
    }
}
