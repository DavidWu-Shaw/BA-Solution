using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class CheckoutFacade : BusinessComponent
    {
        public CheckoutFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<OrderInfoDto> CreateOrdersFromCart(CartDto cart)
        {
            ArgumentValidator.IsNotNull("cart", cart);
            return CreateOrdersFromCart(cart, new ShipToDto());
        }

        public List<OrderInfoDto> CreateOrdersFromCart(CartDto cart, ShipToDto shipTo)
        {
            ArgumentValidator.IsNotNull("cart", cart);
            ArgumentValidator.IsNotNull("shipTo", shipTo);

            List<OrderInfoDto> orders = new List<OrderInfoDto>();
            // validate cart
            bool isValid = Validate(cart);
            if (isValid)
            {
                IEnumerable<object> supplierIds = cart.CartItems.Select(o => o.SupplierId).Distinct();

                int orderNo = 0;
                foreach (object supplierId in supplierIds)
                {
                    orderNo++;
                    OrderInfoDto order = new OrderInfoDto();
                    order.OrderNumber = orderNo.ToString();
                    order.SupplierId = supplierId;
                    order.ShipTo = shipTo;

                    order.OrderItems = new List<OrderItemInfoDto>();
                    foreach (CartItemDto cartItem in cart.CartItems)
                    {
                        // filter by supplierId
                        if (object.Equals(cartItem.SupplierId, supplierId))
                        {
                            order.SupplierName = cartItem.SupplierName;
                            OrderItemInfoDto orderItem = new OrderItemInfoDto();
                            order.OrderItems.Add(orderItem);
                            orderItem.ProductId = cartItem.ProductId;
                            orderItem.ProductName = cartItem.ProductName;
                            orderItem.QtyOrdered = cartItem.QtyOrdered;
                            orderItem.UnitPrice = cartItem.UnitPrice;
                            orderItem.Amount = cartItem.Amount;
                        }
                    }

                    order.QtyOrderedTotal = order.OrderItems.Sum(o => o.QtyOrdered);
                    order.Amount = order.OrderItems.Sum(o => o.Amount);

                    if (order.QtyOrderedTotal > 0)
                    {
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        private bool Validate(CartDto cart)
        {
            cart.QtyOrderedTotal = cart.CartItems.Sum(o => o.QtyOrdered);

            return cart.QtyOrderedTotal > 0;
        }
    }
}
