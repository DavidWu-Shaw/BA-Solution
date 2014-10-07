using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class OrderSystem : BusinessComponent
    {
        public OrderSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllOrder<TDto>(IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveOrdersByCustomer<TDto>(object instanceId, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.SearchByCustomer(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveOrdersBySupplier<TDto>(object instanceId, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.SearchBySupplier(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveOrdersBySupplierAndStatus<TDto>(object instanceId, OrderStatuses status, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("status", status);
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.SearchBySupplierAndStatus(instanceId, (int)status);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveOrdersByContact<TDto>(object instanceId, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.SearchByContact(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewOrder<TDto>(object instanceId, IDataConverter<OrderData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            Order instance = RetrieveOrNew<OrderData, Order, IOrderService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<OrderData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<OrderData> SaveOrder(OrderDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            Order instance = RetrieveOrNew<OrderData, Order, IOrderService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                TransferOrderData(dto, instance);

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<OrderData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        private void TransferOrderData(OrderDto dto, Order instance)
        {
            instance.OrderNumber = dto.OrderNumber;
            instance.CustomerId = dto.CustomerId;
            instance.ContactId = dto.ContactId;
            instance.TimeShipped = dto.TimeShipped;
            instance.TimeShipBy = dto.TimeShipBy;
            instance.TimeCancelBy = dto.TimeCancelBy;
            instance.QtyOrderedTotal = dto.QtyOrderedTotal;
            instance.Amount = dto.Amount;
            instance.CurrencyId = dto.CurrencyId;
            instance.SupplierId = dto.SupplierId;
            instance.ShipToId = dto.ShipToId;
            instance.StatusId = dto.StatusId;
            instance.Notes = dto.Notes;
        }

        internal IFacadeUpdateResult<OrderData> DeleteOrder(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Order instance = query.ToBo<Order>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "OrderCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<OrderData> DeleteOrderItem(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Order parent = query.ToBo<Order>();
                OrderItem child = parent.OrderItems.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.OrderItems.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<OrderData>());
                }
                else
                {
                    AddError(result.ValidationResult, "OrderItemCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "OrderCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<OrderData> SaveOrderItem(object parentId, OrderItemDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var parentQuery = service.Retrieve(parentId);
            if (parentQuery.HasResult)
            {
                Order parent = parentQuery.ToBo<Order>();
                OrderItem child = RetrieveOrNewOrderItem(parent, childDto.Id);
                if (child != null)
                {
                    child.ItemDescription = childDto.ItemDescription;
                    child.ProductId = childDto.ProductId;
                    child.ProductName = childDto.ProductName;
                    child.UnitPrice = childDto.UnitPrice;
                    child.QtyOrdered = childDto.QtyOrdered;
                    child.Amount = childDto.Amount;

                    var saveQuery = service.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<OrderData>());
                }
                else
                {
                    AddError(result.ValidationResult, "OrderItemCannotBeFound");
                }
            }

            return result;
        }

        internal OrderItem RetrieveOrNewOrderItem(Order parent, object childId)
        {
            OrderItem child = null;

            if (childId != null)
            {
                child = parent.OrderItems.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.OrderItems.AddNewBo();
            }

            return child;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (OrderData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.OrderNumber));
                }
            }

            return dataSource;
        }
    }
}
