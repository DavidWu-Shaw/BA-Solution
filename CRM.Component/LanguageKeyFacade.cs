using System.Collections.Generic;
using CRM.Core;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.UoW;

namespace CRM.Component
{
    public class LanguageKeyFacade : BusinessComponent
    {
        public LanguageKeyFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Dictionary<string, string> GetCategoryLanguagePhrasesKeyValue()
        {
            // Create phrase keys with default values
            Dictionary<string, string> phrases = new Dictionary<string, string>();

            IServiceQueryResultList<CategoryData> query = UnitOfWork.GetService<ICategoryService>().GetAll();
            foreach (CategoryData item in query.DataList)
            {
                phrases.Add(string.Format(CommonConst.CategoryKeyFormatString, item.Id), item.Name);
            }

            return phrases;
        }

        public Dictionary<string, string> GetProductLanguagePhrasesKeyValue()
        {
            // Create phrase keys with default values
            Dictionary<string, string> phrases = new Dictionary<string, string>();

            IServiceQueryResultList<ProductData> query = UnitOfWork.GetService<IProductService>().GetAll();
            foreach (ProductData item in query.DataList)
            {
                phrases.Add(string.Format(CommonConst.ProductNameKeyFormatString, item.Id), item.Name);
                phrases.Add(string.Format(CommonConst.ProductDescKeyFormatString, item.Id), item.Description);
            }

            return phrases;
        }

        public Dictionary<string, string> GetSupplierLanguagePhrasesKeyValue()
        {
            // Create phrase keys with default values
            Dictionary<string, string> phrases = new Dictionary<string, string>();

            IServiceQueryResultList<SupplierData> query = UnitOfWork.GetService<ISupplierService>().GetAll();
            foreach (SupplierData item in query.DataList)
            {
                phrases.Add(string.Format(CommonConst.SupplierNameKeyFormatString, item.Id), item.Name);
            }

            return phrases;
        }
    }
}
