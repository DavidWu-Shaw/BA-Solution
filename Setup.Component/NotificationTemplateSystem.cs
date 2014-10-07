using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Core;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class NotificationTemplateSystem : BusinessComponent
    {
        public NotificationTemplateSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal NotificationTemplateData RetrieveTemplateByType(NotificationTypes type)
        {
            ArgumentValidator.IsNotNull("type", type);

            INotificationTemplateService service = UnitOfWork.GetService<INotificationTemplateService>();
            var query = service.RetrieveByType(type);

            if (query.HasResult)
            {
                return query.DataList.First();
            }

            return null;
        }
    }
}
