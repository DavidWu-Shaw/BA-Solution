﻿using System;
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

namespace BA.Web.WebPages.Transaction
{
    public partial class TransactionMgt : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/Transaction/TransactionMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<TransactionDto> _currentInstances;
        public IEnumerable<TransactionDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<TransactionDto>;
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
            ucIList.InstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIList_InstanceRowDeleting);
            ucIList.InstanceRowSaving += new InstanceRowSavingEventHandler(ucIList_InstanceRowSaving);

            PageName = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.ListLabel = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.AllowFilteringByColumn = ucIList.CurrentSubject.AllowListFiltering;

            bool allowModify = CurrentUserContext.IsAdmin || CurrentUserContext.IsEmployee;
            ucIList.AllowAdd = ucIList.CurrentSubject.AllowListAdd && allowModify;
            ucIList.AllowEdit = ucIList.CurrentSubject.AllowListEdit && allowModify;
            ucIList.AllowDelete = ucIList.CurrentSubject.AllowListDelete && allowModify;
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                TransactionFacade facade = new TransactionFacade(uow);
                RetrieveInstances(facade);
            }
        }

        protected void ucIList_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        protected void ucIList_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                TransactionFacade facade = new TransactionFacade(uow);
                IFacadeUpdateResult<TransactionData> result = facade.DeleteTransaction(e.Instance.Id);
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
                TransactionDto project = e.Instance as TransactionDto;
                TransactionFacade facade = new TransactionFacade(uow);
                IFacadeUpdateResult<TransactionData> result = facade.SaveTransaction(project);
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

        private void RetrieveInstances(TransactionFacade facade)
        {
            if (CurrentUserContext.IsAdmin || CurrentUserContext.IsEmployee)
            {
                CurrentInstances = facade.RetrieveAllTransaction(new TransactionConverter());
            }
        }
    }
}