
namespace CRM.ShopComponent.Dto
{
    public class QuoteLineInfoDto
    {
        public object ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TargetPrice { get; set; }

        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        public decimal LineAmount { get; set; }
    }
}
