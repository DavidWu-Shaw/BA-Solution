using CRM.Data;
using Framework.Service;
using Framework.UoW;
using System.Collections.Generic;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface IQuoteService : IUpdateEntityService<QuoteData>
    {
    }
}
