using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using BA.Web.UserControls;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Shop
{
    public partial class CheckoutWizard : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/CheckoutWizard.aspx";

        private const string InstancesStateKey = "InstancesStateKey";

        private IEnumerable<OrderInfoDto> _orders;
        private IEnumerable<OrderInfoDto> Orders
        {
            get
            {
                if (_orders == null)
                {
                    if (IsInViewState(InstancesStateKey))
                    {
                        _orders = GetFromViewState(InstancesStateKey) as IEnumerable<OrderInfoDto>;
                    }
                }

                return _orders;
            }
            set
            {
                _orders = value;
                SaveInViewState(value, InstancesStateKey);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            RequireSSL = true;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.CheckoutWizard.PageName", "Checkout");

            if (!IsPostBack)
            {
                if (WebContext.Current.ApplicationOption.IsTestMode)
                {
                    txtPhone.Text = "514-888-8888 x 1234";
                    txtContact.Text = "Wu";
                    txtAddress.Text = "2239 St-Laurent, Montreal";
                }
                if (!CurrentUserContext.IsCartEmpty)
                {
                    GenerateOrders();
                }
                else
                {
                    Response.Redirect(GetUrl(ShoppingCart.PageUrl));
                }
            }
        }

        private void ClearCart()
        {
            UserCartFacade.ClearCart(CurrentUserContext.ShoppingCart);
        }

        private void ClearCachedOrders()
        {
            Orders = null;
        }

        private void LoadOrderPreview()
        {
            foreach (OrderInfoDto order in Orders)
            {
                OrderView orderViewControl = (OrderView)Page.LoadControl(ServerPath + OrderView.ControlURL);
                // Add the child list control
                phPreview.Controls.Add(orderViewControl);
                // Load data
                orderViewControl.LoadData(order);
            }
        }

        private void LoadOrderConfirmation()
        {
            foreach (OrderInfoDto order in Orders)
            {
                OrderView orderViewControl = (OrderView)Page.LoadControl(ServerPath + OrderView.ControlURL);
                // Add the child list control
                phConfirm.Controls.Add(orderViewControl);
                // Load data
                orderViewControl.LoadData(order);
            }
        }

        private void SaveOrders()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                OrderFacade facade = new OrderFacade(uow);
                FacadeUpdateResult<OrderData> result = facade.SaveNewOrders(Orders);
                if (result.IsSuccessful)
                {
                    List<object> ids = new List<object>();
                    foreach (OrderData savedOrder in result.ResultList)
                    {
                        ids.Add(savedOrder.Id);
                    }
                    // Retrieve order info after saving successful
                    Orders = facade.RetrieveOrdersInfo(ids, new OrderInfoConverter());
                    // Clear cart
                    if (!WebContext.Current.ApplicationOption.IsTestMode)
                    {
                        ClearCart();
                    }
                    LoadOrderConfirmation();
                    // Send notification
                    try
                    {
                        NotificationProcessor.SendOrderConfirmation(txtEmail.Text.Trim(), Orders);
                    }
                    catch (Exception ex)
                    {
                        ProcException(ex, "Order successful. But sending email failed. ");
                    }
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private void GenerateOrders()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CheckoutFacade facade = new CheckoutFacade(uow);
                Orders = facade.CreateOrdersFromCart(CurrentUserContext.ShoppingCart);
            }
        }

        private void UpdateOrders()
        {
            ShippingInfo shippingInfo = CollectShippingInfo();

            foreach (OrderInfoDto order in Orders)
            {
                order.ShipTo.ContactPhone = shippingInfo.ContactPhone;
                order.ShipTo.ContactPerson = shippingInfo.ContactPerson;
                order.ShipTo.AddressLine1 = shippingInfo.Address;
                order.ShipTo.ZipCode = shippingInfo.ZipCode;

                order.TimeShipBy = shippingInfo.TimeShipBy;
                order.Notes = shippingInfo.Notes;
                order.NotificationEmail = shippingInfo.Email;
            }

            LoadOrderPreview();
        }

        private ShippingInfo CollectShippingInfo()
        {
            ShippingInfo shippingInfo = new ShippingInfo();
            shippingInfo.ContactPerson = txtContact.Text.Trim();
            shippingInfo.ContactPhone = txtPhone.Text.Trim();
            shippingInfo.Address = txtAddress.Text.Trim();
            shippingInfo.ZipCode = txtZipCode.Text.Trim();
            shippingInfo.Notes = txtNotes.Text.Trim();
            shippingInfo.Email = txtEmail.Text.Trim();
            if (deShipByTime.SelectedDate.HasValue)
            {
                shippingInfo.TimeShipBy = deShipByTime.SelectedDate.Value;
            }
            return shippingInfo;
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.NextStepIndex == 1)
            {
                UpdateOrders();
            }
        }

        protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.NextStepIndex == 1)
            {
                UpdateOrders();
            }
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Orders != null)
            {
                SaveOrders();
            }
        }

        protected void Wizard1_PreRender(object sender, EventArgs e)
        {
            Repeater steps = Wizard1.FindControl("HeaderContainer").FindControl("rptSteps") as Repeater;

            steps.DataSource = Wizard1.WizardSteps;
            steps.DataBind();
        }

        public string GetHeaderText()
        {
            return string.Format("Step {0}: {1}", Wizard1.ActiveStepIndex + 1, Wizard1.ActiveStep.Title);
        }

        public string GetItemStyle(object obj)
        {
            WizardStep step = obj as WizardStep;
            if (step == null)
            {
                return "";
            }

            int stepIndex = Wizard1.WizardSteps.IndexOf(step);
            if (stepIndex < Wizard1.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > Wizard1.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        private void LocalizeWizard()
        {
            Wizard1.WizardSteps[0].Title = Localize("Shop.CheckoutWizard.stepShipInfo", "Shipping Information");
            Wizard1.WizardSteps[1].Title = Localize("Shop.CheckoutWizard.stepPreview", "Preview");
            Wizard1.WizardSteps[2].Title = Localize("Shop.CheckoutWizard.stepPayment", "Payment and Submit");
            Wizard1.WizardSteps[3].Title = Localize("Shop.CheckoutWizard.stepComplete", "Complete");

            Button btnStartNext = (Button)Wizard1.FindControl("StartNavigationTemplateContainerID$StartNextButton");
            btnStartNext.Text = Localize("Shop.CheckoutWizard.btnStartNext", "Next >>");

            Button btnStepNext = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepNextButton");
            btnStepNext.Text = Localize("Shop.CheckoutWizard.btnStepNext", "Next >>");

            Button btnStepPrevious = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
            btnStepPrevious.Text = Localize("Shop.CheckoutWizard.btnStepPrevious", "<< Previous");

            Button btnFinishPrevious = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishPreviousButton");
            btnFinishPrevious.Text = Localize("Shop.CheckoutWizard.btnFinishPrevious", "<< Previous");

            Button btnFinish = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishButton");
            btnFinish.Text = Localize("Shop.CheckoutWizard.btnFinish", "Submit");
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();

            LocalizeWizard();

            lblTitle.Text = Localize("Shop.CheckoutWizard.lblTitle", "Checkout");
            lblPayment.Text = Localize("Shop.CheckoutWizard.lblPayment", "Please make payment in full when delivered.");
            lblComplete.Text = Localize("Shop.CheckoutWizard.lblComplete", "Order Confirmation");
            lblHint.Text = Localize("Shop.CheckoutWizard.lblHint", "* Write down the Order Number for future reference.");
            lblContact.Text = Localize("Shop.CheckoutWizard.lblContact", "Contact Name:*");
            lblPhone.Text = Localize("Shop.CheckoutWizard.lblPhone", "Contact Phone:*");
            lblAddress.Text = Localize("Shop.CheckoutWizard.lblAddress", "Address:");
            lblPostCode.Text = Localize("Shop.CheckoutWizard.lblPostCode", "Post Code:");
            lblShipBy.Text = Localize("Shop.CheckoutWizard.lblShipBy", "Ship By Time:");
            lblNotes.Text = Localize("Shop.CheckoutWizard.lblNotes", "Notes:");
            lblEmail.Text = Localize("Shop.CheckoutWizard.lblEmail", "Notification Email:");
            txtEmail.ToolTip = Localize("Shop.CheckoutWizard.txtEmailTip", "Order confirmation will be sent to this email address.");
        }
    }
}