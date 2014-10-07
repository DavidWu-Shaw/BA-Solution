using CRM.Business;
using CRM.Data;
using CRM.Service.Contract;
using CRM.ShopComponent.Dto;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    internal class QuoteSystem : BusinessComponent
    {
        public QuoteSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal FacadeUpdateResult<QuoteData> SaveNewQuote(QuoteInfoDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<QuoteData> result = new FacadeUpdateResult<QuoteData>();
            IQuoteService service = UnitOfWork.GetService<IQuoteService>();
            Quote instance = service.CreateNew<Quote>();

            if (result.IsSuccessful)
            {
                TransferQuoteData(dto, instance);

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<QuoteData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        private void TransferQuoteData(QuoteInfoDto dto, Quote instance)
        {
            instance.ProductId = dto.ProductId;
            instance.Email = dto.Email;
            instance.ContactPerson = dto.ContactPerson;
            instance.Phone = dto.Phone;
            instance.Address = dto.Address;
            instance.ZipCode = dto.ZipCode;
            instance.City = dto.City;
            instance.Country = dto.Country;
            instance.TimeCreated = dto.TimeCreated;
            instance.ContactId = dto.ContactId;
            instance.Amount = dto.Amount;
            instance.CurrencyId = dto.CurrencyId;
            instance.Notes = dto.Notes;
        }
    }
}
