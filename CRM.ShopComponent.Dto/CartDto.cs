using System.Collections.Generic;
using Framework.Core;

namespace CRM.ShopComponent.Dto
{
    public class CartDto : BaseDto
    {
        public CartDto()
        {
            CartItems = new List<CartItemDto>();
        }

        public object UserId { get; set; }
        public int QtyOrderedTotal { get; set; }
        public decimal TotalAmount { get; set; }

        public List<CartItemDto> CartItems { get; set; }

        public void Clear()
        {
            QtyOrderedTotal = 0;
            TotalAmount = 0.0m;
            CartItems.Clear();
        }
    }
}
