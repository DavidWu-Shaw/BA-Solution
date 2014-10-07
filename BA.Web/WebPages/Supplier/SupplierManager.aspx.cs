using System;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.UoW;

namespace BA.Web.WebPages.Supplier
{
    public partial class SupplierManager : SetupBasePage
    {
        public const string QrySupplierId = "SupplierId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QrySupplierId].TryToParse<int>();
            }
        }

        private SupplierDto _currentInstance;
        private SupplierDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            SupplierFacade facade = new SupplierFacade(uow);
                            _currentInstance = facade.RetrieveOrNewSupplier(InstanceId, new SupplierConverter());

                            // Retrive Products belong to Supplier
                            ProductFacade productFacade = new ProductFacade(uow);
                            _currentInstance.Products = productFacade.RetrieveProductsBySupplier(InstanceId, new ProductConverter());

                            // Retrive Orders belong to Supplier
                            OrderFacade orderFacade = new OrderFacade(uow);
                            _currentInstance.Orders = orderFacade.RetrieveOrdersBySupplier(InstanceId, new OrderConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as SupplierDto;
                    }
                }

                return _currentInstance;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        protected override bool CheckPagePermission()
        {
            if (CurrentUserContext.IsAdmin || (CurrentUserContext.IsSupplier && object.Equals(CurrentInstance.Id, CurrentUserContext.User.MatchId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.CurrentInstance = CurrentInstance;
            ucIDetail.AllowModify = CurrentUserContext.IsSupplierAdmin || CurrentUserContext.IsAdmin;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Order:
                    e.Instances = CurrentInstance.Orders;
                    break;
                case InstanceTypes.Product:
                    e.Instances = CurrentInstance.Products;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.Product:
                    e.Instance = new ProductDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.Product:
                        ProductFacade facade = new ProductFacade(uow);
                        ProductDto instanceDto = e.Instance as ProductDto;
                        instanceDto.SupplierId = CurrentInstance.Id;
                        // Save data
                        IFacadeUpdateResult<ProductData> result = facade.SaveProduct(instanceDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Products = facade.RetrieveProductsBySupplier(CurrentInstance.Id, new ProductConverter());
                        }
                        else
                        {
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                }
            }
        }

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.Product:
                        ProductFacade facade = new ProductFacade(uow);
                        IFacadeUpdateResult<ProductData> result = facade.DeleteProduct(e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh data in session
                            CurrentInstance.Products = facade.RetrieveProductsBySupplier(CurrentInstance.Id, new ProductConverter());
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                }
            }
        }

        private void SetButtonAttribute()
        {
            if (InstanceId.HasValue)
            {
                //ucIDetail.EditPostBackUrl = GetPageUrl(SDApplicationPage.ProjectEdit, new SDPageParameter(ProjectEdit.QryProjectID, ProjectId));
            }
        }
    }
}