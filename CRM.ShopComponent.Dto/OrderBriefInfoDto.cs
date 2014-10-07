using System;

namespace CRM.ShopComponent.Dto
{
    [Serializable]
    public class OrderBriefInfoDto
    {
        # region Properties name

        public const string FLD_OrderId = "OrderId";
        public const string FLD_OrderNumber = "OrderNumber";
        public const string FLD_StatusId = "StatusId";
        public const string FLD_StatusText = "StatusText";
        public const string FLD_Amount = "Amount";

        # endregion Properties name

        public object OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public int StatusId { get; set; }

        public virtual string StatusText { get; set; }
    }
}
