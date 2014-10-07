using System.Collections.Generic;
using Framework.Data;
using Setup.Core;
using Setup.Data;

namespace Setup.Repository.Contract
{
	public interface INotificationTemplateRepository : IUpdateEntityRepository<NotificationTemplateData>
	{
        IEnumerable<NotificationTemplateData> RetrieveByType(NotificationTypes type);
    }
}
