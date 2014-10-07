using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;
using CRM.Data.Criterias;

namespace CRM.Repository
{
    public class CatalogRepository : NHUpdateEntityRepository<CatalogData>, ICatalogRepository
    {
    }
}
