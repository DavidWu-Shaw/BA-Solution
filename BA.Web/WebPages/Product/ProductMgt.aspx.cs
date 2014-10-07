using System;
using System.Collections.Generic;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;

namespace BA.Web.WebPages.Product
{
    public partial class ProductMgt : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/Product/ProductMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<ProductDto> _currentInstances;
        public IEnumerable<ProductDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<ProductDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetrieveData();
            }

            InitInstanceList();
        }

        private void InitInstanceList()
        {
            ucIList.NeedListInstances += new NeedListInstancesEventHandler(ucIList_NeedListInstances);
            ucIList.InstanceRowNewing += new InstanceRowNewingEventHandler(ucIList_InstanceRowNewing);
            ucIList.InstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIList_InstanceRowDeleting);
            ucIList.InstanceRowSaving += new InstanceRowSavingEventHandler(ucIList_InstanceRowSaving);

            if (!CurrentUserContext.IsAdmin)
            {
                ucIList.ControlledFieldName = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].RelatedSubjectIdField;
            }

            PageName = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.ListLabel = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.AllowFilteringByColumn = ucIList.CurrentSubject.AllowListFiltering;
            
            bool allowModify = CurrentUserContext.IsSupplierAdmin || CurrentUserContext.IsAdmin;
            ucIList.AllowAdd = ucIList.CurrentSubject.AllowListAdd && allowModify;
            ucIList.AllowEdit = ucIList.CurrentSubject.AllowListEdit && allowModify;
            ucIList.AllowDelete = ucIList.CurrentSubject.AllowListDelete && allowModify;
            ucIList.AllowImport = ucIList.CurrentSubject.AllowListImport && allowModify;
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                RetrieveInstances(facade);
            }
        }

        protected void ucIList_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        protected void ucIList_InstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            e.Instance = new ProductDto();
        }

        protected void ucIList_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                IFacadeUpdateResult<ProductData> result = facade.DeleteProduct(e.Instance.Id);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    RetrieveInstances(facade);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected void ucIList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductDto product = e.Instance as ProductDto;
                // Set defaulted value
                if (CurrentUserContext.IsSupplier)
                {
                    product.SupplierId = CurrentUserContext.User.MatchId;
                }
                ProductFacade facade = new ProductFacade(uow);
                IFacadeUpdateResult<ProductData> result = facade.SaveProduct(product);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    RetrieveInstances(facade);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsSupplier || base.CheckPagePermission();
        }

        private void RetrieveInstances(ProductFacade facade)
        {
            if (CurrentUserContext.IsAdmin || CurrentUserContext.IsEmployee)
            {
                CurrentInstances = facade.RetrieveAllProduct(new ProductConverter());
            }
            else if (CurrentUserContext.IsSupplier)
            {
                CurrentInstances = facade.RetrieveProductsBySupplier(CurrentUserContext.User.MatchId, new ProductConverter());
            }
        }
    }
}