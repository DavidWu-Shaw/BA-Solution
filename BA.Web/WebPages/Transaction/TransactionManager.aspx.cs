using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.Component;
using Framework.UoW;

namespace BA.Web.WebPages.Transaction
{
    public partial class TransactionManager : SetupBasePage
    {
        public const string QryTransactionId = "TransactionId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryTransactionId].TryToParse<int>();
            }
        }

        private TransactionDto _currentInstance;
        private TransactionDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            TransactionFacade facade = new TransactionFacade(uow);
                            _currentInstance = facade.RetrieveOrNewTransaction(InstanceId, new TransactionConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as TransactionDto;
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
            return base.CheckPagePermission();
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.AllowModify = CurrentUserContext.IsEmployee || CurrentUserContext.IsAdmin;
            ucIDetail.CurrentInstance = CurrentInstance;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.TransactionItem:
                    e.Instances = CurrentInstance.TransactionItems;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.TransactionItem:
                    e.Instance = new TransactionItemDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.TransactionItem:
                        TransactionFacade facade = new TransactionFacade(uow);
                        TransactionItemDto TransactionItemDto = e.Instance as TransactionItemDto;
                        // Save data
                        IFacadeUpdateResult<TransactionData> result = facade.SaveTransactionItem(CurrentInstance.Id, TransactionItemDto);
                        e.IsSuccessful = result.IsSuccessful;
                        if (result.IsSuccessful)
                        {
                            // Refresh 
                            TransactionDto savedCurrentInstance = result.ToDto(new TransactionConverter());
                            CurrentInstance.TransactionItems = savedCurrentInstance.TransactionItems;
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

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.TransactionItem:
                        TransactionFacade facade = new TransactionFacade(uow);
                        IFacadeUpdateResult<TransactionData> result = facade.DeleteTransactionItem(CurrentInstance.Id, e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh whole list
                            TransactionDto savedCurrentInstance = result.ToDto(new TransactionConverter());
                            CurrentInstance.TransactionItems = savedCurrentInstance.TransactionItems;
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
    }
}