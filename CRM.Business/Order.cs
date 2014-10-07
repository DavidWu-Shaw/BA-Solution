using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;
using CRM.Core;

namespace CRM.Business
{
    public class Order : BusinessObject<OrderData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OrderItems = new ChildBoCollection<OrderData, OrderItem, OrderItemData>(Service, Data.OrderItemsData, this);
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            TimeOrdered = DateTime.Now;
            StatusId = (int)OrderStatuses.Open;
        }

        [ChildCollection]
        public IBusinessObjectCollection<OrderItem> OrderItems
        {
            get;
            private set;
        }

        [StringLength("OrderOrderNumberLength", "The OrderNumber must have a length less than {1}", MaxLength = 50)]
        public string OrderNumber
        {
            get
            {
                return Data.OrderNumber;
            }
            set
            {
                Data.OrderNumber = value;
            }
        }

        public object CustomerId
        {
            get { return Data.CustomerId; }
            set { Data.CustomerId = value; }
        }

        public object ContactId
        {
            get { return Data.ContactId; }
            set { Data.ContactId = value; }
        }

        public DateTime TimeOrdered
        {
            get { return Data.TimeOrdered; }
            private set { Data.TimeOrdered = value; }
        }

        public DateTime? TimeShipped
        {
            get { return Data.TimeShipped; }
            set { Data.TimeShipped = value; }
        }

        public DateTime? TimeShipBy
        {
            get { return Data.TimeShipBy; }
            set { Data.TimeShipBy = value; }
        }

        public DateTime? TimeCancelBy
        {
            get { return Data.TimeCancelBy; }
            set { Data.TimeCancelBy = value; }
        }

        public int QtyOrderedTotal
        {
            get { return Data.QtyOrderedTotal; }
            set { Data.QtyOrderedTotal = value; }
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

        public object SupplierId
        {
            get { return Data.SupplierId; }
            set { Data.SupplierId = value; }
        }
        public object BillToId
        {
            get { return Data.BillToId; }
            set { Data.BillToId = value; }
        }
        public object ShipToId
        {
            get { return Data.ShipToId; }
            set { Data.ShipToId = value; }
        }
        public int StatusId
        {
            get { return Data.StatusId; }
            set { Data.StatusId = value; }
        }
        [StringLength("OrderNotesLength", "The Notes must have a length less than {1}", MaxLength = 500)]
        public string Notes
        {
            get { return Data.Notes; }
            set { Data.Notes = value; }
        }
        [StringLength("OrderShipToContactPhoneLength", "The ShipToContactPhone must have a length less than {1}", MaxLength = 20)]
        public string ShipToContactPhone
        {
            get { return Data.ShipToContactPhone; }
            set { Data.ShipToContactPhone = value; }
        }
        [StringLength("OrderShipToContactPersonLength", "The ShipToContactPerson must have a length less than {1}", MaxLength = 100)]
        public string ShipToContactPerson
        {
            get { return Data.ShipToContactPerson; }
            set { Data.ShipToContactPerson = value; }
        }
        [StringLength("OrderShipToAddressLength", "The ShipToAddress must have a length less than {1}", MaxLength = 100)]
        public string ShipToAddress
        {
            get { return Data.ShipToAddress; }
            set { Data.ShipToAddress = value; }
        }
        [StringLength("OrderShipToZipCodeLength", "The ShipToZipCode must have a length less than {1}", MaxLength = 20)]
        public string ShipToZipCode
        {
            get { return Data.ShipToZipCode; }
            set { Data.ShipToZipCode = value; }
        }
    }
}
