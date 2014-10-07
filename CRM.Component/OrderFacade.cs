using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class OrderFacade : BusinessComponent
    {
        public OrderFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            OrderSystem = new OrderSystem(unitOfWork);
        }

        private OrderSystem OrderSystem { get; set; }

        public List<TDto> RetrieveAllOrder<TDto>(IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = OrderSystem.RetrieveAllOrder(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersBySupplier<TDto>(object id, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = OrderSystem.RetrieveOrdersBySupplier(id, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersBySupplierAndStatus<TDto>(object id, OrderStatuses status, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = OrderSystem.RetrieveOrdersBySupplierAndStatus(id, status, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersByCustomer<TDto>(object id, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = OrderSystem.RetrieveOrdersByCustomer(id, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveOrdersByContact<TDto>(object id, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = OrderSystem.RetrieveOrdersByContact(id, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewOrder<TDto>(object id, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            return OrderSystem.RetrieveOrNewOrder(id, converter);
        }

        public IFacadeUpdateResult<OrderData> SaveOrder(OrderDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.SaveOrder(dto);
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

        public IFacadeUpdateResult<OrderData> DeleteOrder(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.DeleteOrder(id);
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

        public IFacadeUpdateResult<OrderData> DeleteOrderItem(object parentId, object childId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.DeleteOrderItem(parentId, childId);
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

        public IFacadeUpdateResult<OrderData> SaveOrderItem(object parentId, OrderItemDto childDto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<OrderData> result = OrderSystem.SaveOrderItem(parentId, childDto);
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

        public IList<BindingListItem> GetBindingList()
        {
            return OrderSystem.GetBindingList();
        }
    }
}
