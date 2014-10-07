using System.Collections.Generic;
using Framework.Component;
using CRM.Core;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class OrderFacade : BusinessComponent
    {
        public OrderFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            OrderSystem = new OrderSystem(unitOfWork);
        }

        private OrderSystem OrderSystem { get; set; }

        public List<TDto> SearchOrders<TDto>(OrderCriteria criteria, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = OrderSystem.RetrieveOrdersBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersInProcessBySupplier<TDto>(object supplierId, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("supplierId", supplierId);

            OrderCriteria criteria = new OrderCriteria();
            criteria.SupplierId = supplierId;

            List<TDto> instances = OrderSystem.RetrieveOrdersInProcess(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersInProcess<TDto>(IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            OrderCriteria criteria = new OrderCriteria();
            List<TDto> instances = OrderSystem.RetrieveOrdersInProcess(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersBySupplierAndStatus<TDto>(object id, OrderStatuses status, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("id", id);
            ArgumentValidator.IsNotNull("status", status);

            OrderCriteria criteria = new OrderCriteria();
            criteria.SupplierId = id;
            criteria.StatusId = (int)status;

            List<TDto> instances = OrderSystem.RetrieveOrdersBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersByStatus<TDto>(OrderStatuses status, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("status", status);

            OrderCriteria criteria = new OrderCriteria();
            criteria.StatusId = (int)status;

            List<TDto> instances = OrderSystem.RetrieveOrdersBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersByCustomer<TDto>(object id, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("id", id);

            OrderCriteria criteria = new OrderCriteria();
            criteria.CustomerId = id;
            List<TDto> instances = OrderSystem.RetrieveOrdersBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersByOrderNumber<TDto>(string orderNumber, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("orderNumber", orderNumber);

            OrderCriteria criteria = new OrderCriteria();
            criteria.OrderNumber = orderNumber;
            List<TDto> instances = OrderSystem.RetrieveOrdersBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrderInfo<TDto>(object id, IDataConverter<OrderInfoData, TDto> converter)
            where TDto : class
        {
            TDto orderInfo = OrderSystem.RetrieveOrderInfo(id, converter);
            return orderInfo;
        }

        public TDto RetrieveOrderInfo<TDto>(string orderNumber, IDataConverter<OrderInfoData, TDto> converter)
            where TDto : class
        {
            TDto orderInfo = OrderSystem.RetrieveOrderInfo(orderNumber, converter);
            return orderInfo;
        }

        public List<TDto> RetrieveOrdersInfo<TDto>(List<object> ids, IDataConverter<OrderInfoData, TDto> converter)
            where TDto : class
        {
            return OrderSystem.RetrieveOrdersInfo(ids, converter);
        }

        public IFacadeUpdateResult<OrderData> SaveNewOrder(OrderInfoDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.SaveNewOrder(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public FacadeUpdateResult<OrderData> SaveNewOrders(IEnumerable<OrderInfoDto> dtoList)
        {
            UnitOfWork.BeginTransaction();
            FacadeUpdateResult<OrderData> result = OrderSystem.SaveNewOrders(dtoList);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<OrderData> OrderAccept(object orderId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.PerformOrderCommand(orderId, OrderCommand.Accept);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<OrderData> OrderDelivering(object orderId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.PerformOrderCommand(orderId, OrderCommand.Delivering);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<OrderData> OrderComplete(object orderId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.PerformOrderCommand(orderId, OrderCommand.Complete);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<OrderData> OrderIncomplete(object orderId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.PerformOrderCommand(orderId, OrderCommand.Incomplete);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<OrderData> OrderCancel(object orderId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.PerformOrderCommand(orderId, OrderCommand.Cancel);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<OrderData> OrderReject(object orderId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.PerformOrderCommand(orderId, OrderCommand.Reject);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public List<OrderCommand> GetOrderCommands(int statusId)
        {
            List<OrderCommand> commands = new List<OrderCommand>();
            switch ((OrderStatuses)statusId)
            {
                case OrderStatuses.Open:
                    commands.Add(OrderCommand.Accept);
                    commands.Add(OrderCommand.Reject);
                    break;
                case OrderStatuses.Accepted:
                    commands.Add(OrderCommand.Delivering);
                    commands.Add(OrderCommand.Complete);
                    commands.Add(OrderCommand.Incomplete);
                    break;
                case OrderStatuses.Delivering:
                    commands.Add(OrderCommand.Complete);
                    commands.Add(OrderCommand.Incomplete);
                    break;
            }
            return commands;
        }
    }
}
