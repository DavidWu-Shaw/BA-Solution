using Framework.Service;
using Framework.UoW;
using Setup.Core;
using Setup.Data;

namespace Setup.Service.Contract
{
    public interface INotificationTemplateService : IUpdateEntityService<NotificationTemplateData>
    {
        IServiceQueryResultList<NotificationTemplateData> RetrieveByType(NotificationTypes type);

    }
}
