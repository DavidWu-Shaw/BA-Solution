using System;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.Core;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.UserControls
{
    public partial class OrderProcessor : BaseControl
    {
        public const string ControlURL = "/UserControls/OrderProcessor.ascx";

        private string UniqueInstanceStateKey { get { return string.Format("{0}_{1}", UniqueID, InstanceStateKey); } }

        public event InstanceChangedEventHandler OrderChanged;

        private OrderInfoDto _order;
        public OrderInfoDto Order
        {
            get
            {
                if (_order == null && IsPostBack && IsInSession(UniqueInstanceStateKey))
                {
                    _order = GetFromSession(UniqueInstanceStateKey) as OrderInfoDto;
                }

                return _order;
            }
            set
            {
                _order = value;
                SaveInSession(_order, UniqueInstanceStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && Order != null)
            {
                LoadOrderCommand();
            }
        }

        public void LoadData()
        {
            ucOrder.LoadData(Order);
            LoadOrderCommand();
        }

        private void LoadOrderCommand()
        {
            divButtons.Controls.Clear();
            foreach (OrderCommand command in Order.OrderCommands)
            {
                Button btn = new Button();
                divButtons.Controls.Add(btn);
                btn.ID = command.ToString();
                btn.Text = command.ToString();
                btn.Attributes.Add("class", "clsButton");
                btn.Click += new EventHandler(btn_Click);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            OrderCommand command = (OrderCommand)Enum.Parse(typeof(OrderCommand), btn.ID, true);

            ProcOrder(command);
        }

        private void ProcOrder(OrderCommand command)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                IFacadeUpdateResult<OrderData> result = null; 

                switch (command)
                {
                    case OrderCommand.Accept:
                        result = facade.OrderAccept(Order.OrderId);
                        break;
                    case OrderCommand.Reject:
                        result = facade.OrderReject(Order.OrderId);
                        break;
                    case OrderCommand.Delivering:
                        result = facade.OrderDelivering(Order.OrderId);
                        break;
                    case OrderCommand.Complete:
                        result = facade.OrderComplete(Order.OrderId);
                        break;
                    case OrderCommand.Incomplete:
                        result = facade.OrderIncomplete(Order.OrderId);
                        break;
                }

                if (result.IsSuccessful)
                {
                    // retrieve saved order info
                    // remove this line on May.23, move retrieve action to caller page
                    //Order = facade.RetrieveOrderInfo(Order.OrderId, new OrderInfoConverter());
                    if (OrderChanged != null)
                    {
                        InstanceChangedEventArgs arg = new InstanceChangedEventArgs(Order);
                        OrderChanged(this, arg);
                    }
                }
                else
                {
                    // Deal with Update result
                    //ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }
    }
}