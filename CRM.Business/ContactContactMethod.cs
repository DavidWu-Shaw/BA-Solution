using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class ContactContactMethod : ChildBusinessObject<Contact, ContactContactMethodData>
    {
        [RequiredField("ContactContactMethodContactMethodValueRequired", "The ContactMethodValue must be defined.")]
        [StringLength("ContactContactMethodContactMethodValueLength", "The ContactMethodValue must have a length less than {1}", MaxLength = 100)]
        public string ContactMethodValue
        {
            get
            {
                return Data.ContactMethodValue;
            }
            set
            {
                Data.ContactMethodValue = value;
            }
        }

        [RequiredField("ContactContactMethodContactMethodTypeIdRequired", "The ContactMethodTypeId must be defined.")]
        public object ContactMethodTypeId
        {
            get
            {
                return Data.ContactMethodTypeId;
            }
            set
            {
                Data.ContactMethodTypeId = value;
            }
        }
    }
}
