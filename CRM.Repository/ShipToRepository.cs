using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class ShipToRepository : NHUpdateEntityRepository<ShipToData>, IShipToRepository
    {
        public IEnumerable<ShipToData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<ShipToData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ShipToData>();
                query.AddExpressionEq<ShipToData, object>(o => o.CustomerId, customerId);

                result = query.List<ShipToData>();
            });

            return result;
        }

        public IEnumerable<ShipToData> SearchByContactPhone(string phone)
        {
            ArgumentValidator.IsNotNull("phone", phone);

            IEnumerable<ShipToData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ShipToData>();
                query.AddExpressionEq<ShipToData, object>(o => o.ContactPhone, phone);

                result = query.List<ShipToData>();
            });

            return result;
        }
    }
}
