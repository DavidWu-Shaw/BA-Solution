using System.Collections.Generic;
using Framework.Component;
using CRM.ShopComponent.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class QuoteFacade : BusinessComponent
    {
        public QuoteFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            QuoteSystem = new QuoteSystem(unitOfWork);
        }

        private QuoteSystem QuoteSystem { get; set; }

        public QuoteInfoDto CreateQuote(QuoteInfoDto dto)
        {
            ProductSystem productSystem = new ProductSystem(UnitOfWork);
            ProductInfoData productData = productSystem.RetrieveProductInfo(dto.ProductId);
            // TODO: fill some fields
            dto.ProductName = productData.Name;
            dto.Amount = productData.UnitPrice * 1;

            return dto;
        }

        public QuoteInfoDto CreateQuote(object productId)
        {
            ArgumentValidator.IsNotNull("productId", productId);

            QuoteInfoDto dto = new QuoteInfoDto();
            ProductSystem productSystem = new ProductSystem(UnitOfWork);
            ProductInfoData productData = productSystem.RetrieveProductInfo(productId);
            dto.ProductId = productId;
            dto.ProductName = productData.Name;
            dto.Amount = productData.UnitPrice * 1;

            return dto;
        }

        public FacadeUpdateResult<QuoteData> SaveNewQuote(QuoteInfoDto dto)
        {
            UnitOfWork.BeginTransaction();
            FacadeUpdateResult<QuoteData> result = QuoteSystem.SaveNewQuote(dto);
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
    }
}
