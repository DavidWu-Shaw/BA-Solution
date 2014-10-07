using Framework.Data;

namespace CRM.Data
{
    public class ContactContactMethodData : ChildDataObject
    {
        public virtual object ContactMethodTypeId { get; set; }
        public virtual string ContactMethodValue { get; set; }
    }
}
