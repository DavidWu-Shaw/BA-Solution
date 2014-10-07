using Framework.Core;

namespace CRM.Component.Dto
{
    public class ContactContactMethodDto : BaseDto
    {
        public object ContactMethodTypeId { get; set; }
        public string ContactMethodValue { get; set; }
    }
}
