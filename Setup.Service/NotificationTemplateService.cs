using Framework.Service;
using Framework.UoW;
using Setup.Core;
using Setup.Data;
using Setup.Repository.Contract;
using Setup.Service.Contract;


namespace Setup.Service
{
    public class NotificationTemplateService : UpdateEntityService<INotificationTemplateRepository, NotificationTemplateData>, INotificationTemplateService
	{
        public IServiceQueryResultList<NotificationTemplateData> RetrieveByType(NotificationTypes type)
        {
            var result = Repository.RetrieveByType(type);
            return ServiceResultFactory.BuildServiceQueryResult(result);
        }
    }
}
