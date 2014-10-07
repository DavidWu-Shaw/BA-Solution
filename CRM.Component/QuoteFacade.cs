using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class QuoteFacade : BusinessComponent
    {
        public QuoteFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            QuoteSystem = new QuoteSystem(unitOfWork);
        }

        private QuoteSystem QuoteSystem { get; set; }

        public List<TDto> RetrieveAllQuote<TDto>(IDataConverter<QuoteData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = QuoteSystem.RetrieveAllQuote(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewQuote<TDto>(object id, IDataConverter<QuoteData, TDto> converter)
            where TDto : class
        {
            return QuoteSystem.RetrieveOrNewQuote(id, converter);
        }

        public IFacadeUpdateResult<QuoteData> SaveQuote(QuoteDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<QuoteData> result = QuoteSystem.SaveQuote(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<QuoteData> DeleteQuote(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<QuoteData> result = QuoteSystem.DeleteQuote(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<QuoteData> DeleteQuoteLine(object parentId, object childId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<QuoteData> result = QuoteSystem.DeleteQuoteLine(parentId, childId);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<QuoteData> SaveQuoteLine(object parentId, QuoteLineDto childDto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<QuoteData> result = QuoteSystem.SaveQuoteLine(parentId, childDto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IList<BindingListItem> GetBindingList()
        {
            return QuoteSystem.GetBindingList();
        }
    }
}
