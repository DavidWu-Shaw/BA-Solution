using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;
using CRM.Core;

namespace CRM.Business
{
    public class Quote : BusinessObject<QuoteData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            QuoteLines = new ChildBoCollection<QuoteData, QuoteLine, QuoteLineData>(Service, Data.QuoteLinesData, this);
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            StatusId = (int)QuoteStatuses.Open;
        }

        [ChildCollection]
        public IBusinessObjectCollection<QuoteLine> QuoteLines
        {
            get;
            private set;
        }

        [StringLength("QuoteReferenceNumberLength", "The ReferenceNumber must have a length less than {1}", MaxLength = 50)]
        public string ReferenceNumber
        {
            get { return Data.ReferenceNumber; }
            set { Data.ReferenceNumber = value; }
        }

        public object ProductId
        {
            get { return Data.ProductId; }
            set { Data.ProductId = value; }
        }

        public object ContactId
        {
            get { return Data.ContactId; }
            set { Data.ContactId = value; }
        }

        public DateTime TimeCreated
        {
            get { return Data.TimeCreated; }
            set { Data.TimeCreated = value; }
        }

        public int StatusId
        {
            get { return Data.StatusId; }
            set { Data.StatusId = value; }
        }

        public int QuoteTypeId
        {
            get { return Data.QuoteTypeId; }
            set { Data.QuoteTypeId = value; }
        }

        public decimal Amount
        {
            get { return Data.Amount; }
            set { Data.Amount = value; }
        }

        public object CurrencyId
        {
            get { return Data.CurrencyId; }
            set { Data.CurrencyId = value; }
        }

        [StringLength("QuoteNotesLength", "The Notes must have a length less than {1}", MaxLength = 500)]
        public string Notes
        {
            get { return Data.Notes; }
            set { Data.Notes = value; }
        }
        [StringLength("QuoteContactPersonLength", "The ContactPerson must have a length less than {1}", MaxLength = 100)]
        public string ContactPerson
        {
            get { return Data.ContactPerson; }
            set { Data.ContactPerson = value; }
        }
        [StringLength("QuoteEmailLength", "The Email must have a length less than {1}", MaxLength = 50)]
        public string Email
        {
            get { return Data.Email; }
            set { Data.Email = value; }
        }
        [StringLength("QuoteAddressLength", "The Address must have a length less than {1}", MaxLength = 100)]
        public string Address
        {
            get { return Data.Address; }
            set { Data.Address = value; }
        }
        [StringLength("QuoteZipCodeLength", "The ZipCode must have a length less than {1}", MaxLength = 20)]
        public string ZipCode
        {
            get { return Data.ZipCode; }
            set { Data.ZipCode = value; }
        }

        [StringLength("QuotePhoneLength", "The Phone must have a length less than {1}", MaxLength = 50)]
        public string Phone
        {
            get
            {
                return Data.Phone;
            }
            set
            {
                Data.Phone = value;
            }
        }

        [StringLength("QuoteCityLength", "The City must have a length less than {1}", MaxLength = 50)]
        public string City
        {
            get
            {
                return Data.City;
            }
            set
            {
                Data.City = value;
            }
        }
        [StringLength("QuoteCountryLength", "The Country must have a length less than {1}", MaxLength = 50)]
        public string Country
        {
            get
            {
                return Data.Country;
            }
            set
            {
                Data.Country = value;
            }
        }
    }
}
