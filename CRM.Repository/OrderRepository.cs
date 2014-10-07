using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;
using CRM.Data.Criterias;

namespace CRM.Repository
{
    public class OrderRepository : NHUpdateEntityRepository<OrderData>, IOrderRepository
    {
        public IEnumerable<OrderData> SearchBySupplier(object supplierId)
        {
            ArgumentValidator.IsNotNull("supplierId", supplierId);

            IEnumerable<OrderData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<OrderData>();
                query.AddExpressionEq<OrderData, object>(o => o.SupplierId, supplierId);

                result = query.List<OrderData>();
            });

            return result;
        }

        public IEnumerable<OrderData> SearchBySupplierAndStatus(object supplierId, int statusId)
        {
            ArgumentValidator.IsNotNull("supplierId", supplierId);
            ArgumentValidator.IsNotNull("statusId", statusId);

            IEnumerable<OrderData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<OrderData>();
                query.AddExpressionEq<OrderData, object>(o => o.SupplierId, supplierId);
                query.AddExpressionEq<OrderData, int>(o => o.StatusId, statusId);

                result = query.List<OrderData>();
            });

            return result;
        }


        public IEnumerable<OrderData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<OrderData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<OrderData>();
                query.AddExpressionEq<OrderData, object>(o => o.CustomerId, customerId);

                result = query.List<OrderData>();
            });

            return result;
        }

        public IEnumerable<OrderData> SearchByContact(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<OrderData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<OrderData>();
                query.AddExpressionEq<OrderData, object>(o => o.ContactId, id);

                result = query.List<OrderData>();
            });

            return result;
        }

        public IEnumerable<OrderBriefInfoData> Search(OrderCriteria criteria)
        {
            IEnumerable<OrderBriefInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetOrderBriefInfoList");
                if (criteria.SupplierId != null)
                {
                    query.SetParameter("SupplierId", criteria.SupplierId);
                }
                else
                {
                    query.SetString("SupplierId", null);
                }
                if (criteria.CustomerId != null)
                {
                    query.SetParameter("CustomerId", criteria.CustomerId);
                }
                else
                {
                    query.SetString("CustomerId", null);
                }
                query.SetNullableInt32("StatusId", criteria.StatusId);

                if (string.IsNullOrEmpty(criteria.OrderNumber))
                {
                    query.SetString("OrderNumber", null);
                }
                else
                {
                    query.SetString("OrderNumber", criteria.OrderNumber.Trim());
                }

                result = query.List<OrderBriefInfoData>();
            });

            return result;
        }

        public IEnumerable<OrderBriefInfoData> GetOrdersInProcess(OrderCriteria criteria)
        {
            IEnumerable<OrderBriefInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetOrdersInProcess");
                if (criteria.SupplierId != null)
                {
                    query.SetParameter("SupplierId", criteria.SupplierId);
                }
                else
                {
                    query.SetString("SupplierId", null);
                }
                if (criteria.CustomerId != null)
                {
                    query.SetParameter("CustomerId", criteria.CustomerId);
                }
                else
                {
                    query.SetString("CustomerId", null);
                }

                result = query.List<OrderBriefInfoData>();
            });

            return result;
        }

        public OrderInfoData GetOrderInfo(object orderId)
        {
            OrderInfoData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetOrderInfo");
                query.SetParameter("OrderId", orderId);
                query.SetString("OrderNumber", null);
                result = query.UniqueResult<OrderInfoData>();

                if (result != null)
                {
                    IQuery queryDetail = CurrentSession.GetNamedQuery("GetOrderItemInfoList");
                    queryDetail.SetParameter("OrderId", orderId);
                    result.OrderItemInfosData = queryDetail.List<OrderItemInfoData>();
                }
            });

            return result;
        }

        public OrderInfoData GetOrderInfo(string orderNumber)
        {
            OrderInfoData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetOrderInfo");
                query.SetParameter("OrderNumber", orderNumber);
                query.SetString("OrderId", null);
                result = query.UniqueResult<OrderInfoData>();

                if (result != null)
                {
                    IQuery queryDetail = CurrentSession.GetNamedQuery("GetOrderItemInfoList");
                    queryDetail.SetParameter("OrderId", result.OrderId);
                    result.OrderItemInfosData = queryDetail.List<OrderItemInfoData>();
                }
            });

            return result;
        }

        public IEnumerable<OrderInfoData> GetOrdersInfo(IEnumerable<object> orderIds)
        {
            List<OrderInfoData> orders = new List<OrderInfoData>();

            foreach (object orderId in orderIds)
            {
                OrderInfoData order = GetOrderInfo(orderId);
                orders.Add(order);
            }

            return orders;
        }
    }
}
