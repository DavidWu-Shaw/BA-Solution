using System.Collections.Generic;
using Framework.Data;
using Framework.Data.NHibernate;
using NHibernate;
using Setup.Core;
using Setup.Data;
using Setup.Repository.Contract;


namespace Setup.Repository
{
    public class NotificationTemplateRepository : NHUpdateEntityRepository<NotificationTemplateData>, INotificationTemplateRepository
    {
        public IEnumerable<NotificationTemplateData> RetrieveByType(NotificationTypes type)
        {
            IEnumerable<NotificationTemplateData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
                {
                    result = CurrentSession.CreateCriteria<NotificationTemplateData>()
                        .AddExpressionEq<NotificationTemplateData, int>(o => o.NotificationTypeId, (int)type)
                        .List<NotificationTemplateData>();
                });

            return result;
        }
    }
}
