using System;

namespace CRM.ShopComponent.Dto
{
    [Serializable]
    public class ShipToDto
    {
        public object Id { get; set; }
        public string Code { get; set; }
        public object CustomerId { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
