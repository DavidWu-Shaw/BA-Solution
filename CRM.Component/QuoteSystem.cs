using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class QuoteSystem : BusinessComponent
    {
        public QuoteSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllQuote<TDto>(IDataConverter<QuoteData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewQuote<TDto>(object instanceId, IDataConverter<QuoteData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            FacadeUpdateResult<QuoteData> result = new FacadeUpdateResult<QuoteData>();
            Quote instance = RetrieveOrNew<QuoteData, Quote, IQuoteService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<QuoteData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<QuoteData> SaveQuote(QuoteDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<QuoteData> result = new FacadeUpdateResult<QuoteData>();
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            Quote instance = RetrieveOrNew<QuoteData, Quote, IQuoteService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                TransferQuoteData(dto, instance);

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<QuoteData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        private void TransferQuoteData(QuoteDto dto, Quote instance)
        {
            instance.ReferenceNumber = dto.ReferenceNumber;
            instance.ContactId = dto.ContactId;
            instance.Amount = dto.Amount;
            instance.CurrencyId = dto.CurrencyId;
            instance.Notes = dto.Notes;
        }

        internal IFacadeUpdateResult<QuoteData> DeleteQuote(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<QuoteData> result = new FacadeUpdateResult<QuoteData>();
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Quote instance = query.ToBo<Quote>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "QuoteCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<QuoteData> DeleteQuoteLine(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<QuoteData> result = new FacadeUpdateResult<QuoteData>();
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Quote parent = query.ToBo<Quote>();
                QuoteLine child = parent.QuoteLines.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.QuoteLines.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<QuoteData>());
                }
                else
                {
                    AddError(result.ValidationResult, "QuoteLineCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "QuoteCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<QuoteData> SaveQuoteLine(object parentId, QuoteLineDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<QuoteData> result = new FacadeUpdateResult<QuoteData>();
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            var parentQuery = service.Retrieve(parentId);
            if (parentQuery.HasResult)
            {
                Quote parent = parentQuery.ToBo<Quote>();
                QuoteLine child = RetrieveOrNewQuoteLine(parent, childDto.Id);
                if (child != null)
                {
                    child.ProductId = childDto.ProductId;
                    child.Quantity = childDto.Quantity;
                    child.TargetPrice = childDto.TargetPrice;

                    var saveQuery = service.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<QuoteData>());
                }
                else
                {
                    AddError(result.ValidationResult, "QuoteLineCannotBeFound");
                }
            }

            return result;
        }

        internal QuoteLine RetrieveOrNewQuoteLine(Quote parent, object childId)
        {
            QuoteLine child = null;

            if (childId != null)
            {
                child = parent.QuoteLines.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.QuoteLines.AddNewBo();
            }

            return child;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (QuoteData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.ReferenceNumber));
                }
            }

            return dataSource;
        }
    }
}
