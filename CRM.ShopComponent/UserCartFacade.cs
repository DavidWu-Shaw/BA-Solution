using System.Linq;
using Framework.Component;
using CRM.Data;
using CRM.Service.Contract;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class UserCartFacade : BusinessComponent
    {
        public UserCartFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ProductSystem = new ProductSystem(unitOfWork);
        }

        private ProductSystem ProductSystem { get; set; }

        public static void ClearCart(CartDto cart)
        {
            cart.QtyOrderedTotal = 0;
            cart.TotalAmount = 0;
            cart.CartItems.Clear();
        }

        public void AddToCart(CartDto cart, object productId, IDataConverter<ProductInfoData, CartItemDto> converter)
        {
            AddToCart(cart, productId, 1, converter);
        }

        public void AddToCart(CartDto cart, object productId, int quantity, IDataConverter<ProductInfoData, CartItemDto> converter)
        {
            // Check if already exist
            CartItemDto cartItem = cart.CartItems.SingleOrDefault(o => object.Equals(o.ProductId, productId));
            if (cartItem != null)
            {
                cartItem.QtyOrdered += quantity;
            }
            else
            {
                IProductService service = UnitOfWork.GetService<IProductService>();
                var query = service.RetrieveProductInfo(productId);

                if (query.HasResult)
                {
                    cartItem = converter.Convert(query.Data);
                    cartItem.QtyOrdered = quantity;
                    cart.CartItems.Add(cartItem);
                }
            }
            cartItem.Amount = cartItem.UnitPrice * cartItem.QtyOrdered;
            CalculateCart(cart);
        }

        public void RemoveFromCart(CartDto cart, object productId)
        {
            CartItemDto cartItem = cart.CartItems.SingleOrDefault(o => object.Equals(o.ProductId, productId));
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                CalculateCart(cart);
            }
        }

        private void CalculateCart(CartDto cart)
        {
            // Calculate Cart Total
            cart.QtyOrderedTotal = cart.CartItems.Sum(o => o.QtyOrdered);
            cart.TotalAmount = cart.CartItems.Sum(o => o.Amount);
        }
    }
}
