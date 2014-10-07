using System;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Shop.Converters;
using CRM.Data;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.Shop
{
    public partial class QuoteCreationWizard : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/QuoteCreationWizard.aspx";
        public const string QryProductId = "ProductId";

        private int? ProductId
        {
            get
            {
                return Request.QueryString[QryProductId].TryToParse<int>();
            }
        }

        private QuoteInfoDto _currentInstance;
        public QuoteInfoDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as QuoteInfoDto;
                    }
                }

                return _currentInstance;
            }
            set
            {
                _currentInstance = value;
                SaveInSession(_currentInstance, InstanceStateKey);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            RequireSSL = true;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.QuoteCreationWizard.PageName", "Quote Request");

            if (!IsPostBack)
            {
                if (WebContext.Current.ApplicationOption.IsTestMode)
                {
                    txtEmail.Text = WebContext.Current.ApplicationOption.SiteCoordinatorEmail;
                }
                RetrieveProductData();
            }
        }

        private void RetrieveProductData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                ProductInfoDto product = facade.RetrieveProductInfo(ProductId, new ProductInfoConverter(CurrentUserContext.CurrentLanguage.Id));
                lblTitle.Text = product.Name;
            }
        }

        private void CreateQuote()
        {
            QuoteInfoDto quote = new QuoteInfoDto();
            CollectQuoteInfo(quote);

            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                QuoteFacade facade = new QuoteFacade(uow);
                CurrentInstance = facade.CreateQuote(quote);
            }
        }

        private void SaveQuote()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                QuoteFacade facade = new QuoteFacade(uow);
                FacadeUpdateResult<QuoteData> result = facade.SaveNewQuote(CurrentInstance);
                if (result.IsSuccessful)
                {
                    // Load saved Quote
                    CurrentInstance.QuoteId = result.Result.Id;
                    CurrentInstance.StatusId = result.Result.StatusId;
                    ucSavedQuote.LoadData(CurrentInstance);
                    // Send notification
                    try
                    {
                        NotificationProcessor.SendQuoteCreation(CurrentInstance);
                    }
                    catch (Exception ex)
                    {
                        ProcException(ex, "Quote creation successful. But sending email failed. ");
                    }
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private void CollectQuoteInfo(QuoteInfoDto quote)
        {
            quote.ProductId = ProductId;
            quote.TimeCreated = DateTime.Now;
            quote.ContactPerson = txtContact.Text.Trim();
            quote.Email = txtEmail.Text.Trim();
            quote.Phone = txtPhone.Text.Trim();
            quote.Address = txtAddress.Text.Trim();
            quote.ZipCode = txtZipCode.Text.Trim();
            quote.Notes = txtNotes.Text.Trim();
        }

        private void QuotePreview()
        {
            CreateQuote();

            ucQuote.LoadData(CurrentInstance);
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.NextStepIndex == 1)
            {
                QuotePreview();
            }
        }

        protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.NextStepIndex == 1)
            {
                QuotePreview();
            }
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            SaveQuote();
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
            Wizard1.WizardSteps[0].Title = Localize("Shop.QuoteCreationWizard.stepContactInfo", "Contact Information");
            Wizard1.WizardSteps[1].Title = Localize("Shop.QuoteCreationWizard.stepSubmit", "Confirm and Submit");
            Wizard1.WizardSteps[2].Title = Localize("Shop.QuoteCreationWizard.stepComplete", "Complete");

            Button btnStartNext = (Button)Wizard1.FindControl("StartNavigationTemplateContainerID$StartNextButton");
            btnStartNext.Text = Localize("Shop.QuoteCreationWizard.btnStartNext", "Next >>");

            Button btnStepNext = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepNextButton");
            btnStepNext.Text = Localize("Shop.QuoteCreationWizard.btnStepNext", "Next >>");

            Button btnStepPrevious = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
            btnStepPrevious.Text = Localize("Shop.QuoteCreationWizard.btnStepPrevious", "<< Previous");

            Button btnFinishPrevious = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishPreviousButton");
            btnFinishPrevious.Text = Localize("Shop.QuoteCreationWizard.btnFinishPrevious", "<< Previous");

            Button btnFinish = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishButton");
            btnFinish.Text = Localize("Shop.QuoteCreationWizard.btnFinish", "Submit to contact us");
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();

            LocalizeWizard();

            lblComplete.Text = Localize("Shop.QuoteCreationWizard.lblComplete", "Quote Confirmation");
            lblEmail.Text = Localize("Shop.QuoteCreationWizard.lblEmail", "Contact Email:");
            lblContact.Text = Localize("Shop.QuoteCreationWizard.lblContact", "Contact Name:");
            lblPhone.Text = Localize("Shop.QuoteCreationWizard.lblPhone", "Contact Phone:");
            lblAddress.Text = Localize("Shop.QuoteCreationWizard.lblAddress", "Address:");
            lblPostCode.Text = Localize("Shop.QuoteCreationWizard.lblPostCode", "Post Code:");
            lblNotes.Text = Localize("Shop.QuoteCreationWizard.lblNotes", "Notes:");
            txtEmail.ToolTip = Localize("Shop.QuoteCreationWizard.txtEmailTip", "Quote information will be sent to this email address.");
        }
    }
}