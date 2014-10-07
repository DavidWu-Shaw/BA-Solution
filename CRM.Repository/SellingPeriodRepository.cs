using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class SellingPeriodRepository : NHUpdateEntityRepository<SellingPeriodData>, ISellingPeriodRepository
    {
    }
}
