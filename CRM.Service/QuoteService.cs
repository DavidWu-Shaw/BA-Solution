using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class QuoteService : UpdateEntityService<IQuoteRepository, QuoteData>, IQuoteService
    {
    }
}
