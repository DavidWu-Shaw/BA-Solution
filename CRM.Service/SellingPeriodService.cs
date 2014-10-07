using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class SellingPeriodService : UpdateEntityService<ISellingPeriodRepository, SellingPeriodData>, ISellingPeriodService
    {
    }
}
