using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Business;
using CRM.Core;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.Service.Contract;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;
using Framework.Validation;

namespace CRM.ShopComponent
{
    internal class OrderSystem : BusinessComponent
    {
        public OrderSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveOrdersBySearch<TDto>(OrderCriteria criteria, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("criteria", criteria);
            ArgumentValidator.IsNotNull("converter", converter);

            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveOrdersInProcess<TDto>(OrderCriteria criteria, IDataConverter<OrderBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("criteria", criteria);
            ArgumentValidator.IsNotNull("converter", converter);

            IOrderService service = UnitOfWork.GetService<IOrderService>();

            var query = service.GetOrdersInProcess(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrderInfo<TDto>(object instanceId, IDataConverter<OrderInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var query = service.GetOrderInfo(instanceId);
            if (query.HasResult)
            {
                return query.DataToDto(converter);
            }

            return null;
        }

        internal TDto RetrieveOrderInfo<TDto>(string orderNumber, IDataConverter<OrderInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("orderNumber", orderNumber);
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var query = service.GetOrderInfo(orderNumber);
            if (query.HasResult)
            {
                return query.DataToDto(converter);
            }

            return null;
        }

        internal List<TDto> RetrieveOrdersInfo<TDto>(List<object> instanceIds, IDataConverter<OrderInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceIds", instanceIds);
            ArgumentValidator.IsNotNull("converter", converter);
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            var query = service.GetOrdersInfo(instanceIds);
            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal IFacadeUpdateResult<OrderData> SaveNewOrder(OrderInfoDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            // Create Order
            Order instance = service.CreateNew<Order>();
            TransferOrderData(dto, instance);
            // Create OrderItems
            foreach (OrderItemInfoDto itemDto in dto.OrderItems)
            {
                OrderItem item = instance.OrderItems.AddNewBo();
                TransferOrderItemData(itemDto, item);
            }
            // Save
            var saveQuery = service.Save(instance);

            result.AttachResult(instance.RetrieveData<OrderData>());
            result.Merge(saveQuery);

            return result;
        }

        private void TransferOrderData(OrderInfoDto dto, Order instance)
        {
            if (dto.ShipToId == null)
            {
                instance.ShipToContactPhone = dto.ShipTo.ContactPhone;
                instance.ShipToContactPerson = dto.ShipTo.ContactPerson;
                instance.ShipToAddress = dto.ShipTo.AddressLine1;
                instance.ShipToZipCode = dto.ShipTo.ZipCode;
            }
            instance.OrderNumber = ComposeOrderNumber(instance.ShipToContactPhone, instance.TimeOrdered, dto.OrderNumber);
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
            instance.Notes = dto.Notes;
        }

        private string ComposeOrderNumber(string phone, DateTime timeOrdered, string orderNo)
        {
            return string.Format("{0}-{1}-{2}", phone, timeOrdered.ToString("yyMMddHHmm"), orderNo);
        }

        private void TransferOrderItemData(OrderItemInfoDto dto, OrderItem instance)
        {
            instance.ItemDescription = dto.ItemDescription;
            instance.ProductId = dto.ProductId;
            instance.ProductName = dto.ProductName;
            instance.UnitPrice = dto.UnitPrice;
            instance.QtyOrdered = dto.QtyOrdered;
            instance.Amount = dto.Amount;
        }

        internal FacadeUpdateResult<OrderData> SaveNewOrders(IEnumerable<OrderInfoDto> dtoList)
        {
            ArgumentValidator.IsNotNull("dtoList", dtoList);

            List<Order> instances = new List<Order>();

            IOrderService service = UnitOfWork.GetService<IOrderService>();

            foreach (OrderInfoDto orderDto in dtoList)
            {
                // Create Order
                Order instance = service.CreateNew<Order>();
                instances.Add(instance);
                TransferOrderData(orderDto, instance);
                // Create OrderItems
                foreach (OrderItemInfoDto itemDto in orderDto.OrderItems)
                {
                    OrderItem item = instance.OrderItems.AddNewBo();
                    TransferOrderItemData(itemDto, item);
                }
            }
            // Save Orders
            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            var saveQuery = service.SaveAll(instances);
            result.Merge(saveQuery);

            List<OrderData> dataList = new List<OrderData>();
            foreach (Order instance in instances)
            {
                dataList.Add(instance.RetrieveData<OrderData>());
            }
            result.AttachResult(dataList);

            return result;
        }

        internal IFacadeUpdateResult<OrderData> PerformOrderCommand(object orderId, OrderCommand command)
        {
            ArgumentValidator.IsNotNull("orderId", orderId);

            FacadeUpdateResult<OrderData> result = new FacadeUpdateResult<OrderData>();
            IOrderService service = UnitOfWork.GetService<IOrderService>();
            Order instance = RetrieveOrNew<OrderData, Order, IOrderService>(result.ValidationResult, orderId);
            if (result.IsSuccessful)
            {
                ValidateOrderCommand(instance, command, result.ValidationResult);
                if (result.IsSuccessful)
                {
                    instance.StatusId = (int)command;
                    var saveQuery = service.Save(instance);

                    result.AttachResult(instance.RetrieveData<OrderData>());
                    result.Merge(saveQuery);
                }
            }

            return result;
        }

        private void ValidateOrderCommand(Order instance, OrderCommand command, ValidationResult validationResult)
        {

        }
    }
}
