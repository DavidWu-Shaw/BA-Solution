using Framework.Component;
using Framework.UoW;
using Setup.Core;
using Setup.Data;

namespace Setup.Component
{
    public class NotificationTemplateFacade : BusinessComponent
    {
        public NotificationTemplateFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            NotificationTemplateSystem = new NotificationTemplateSystem(unitOfWork);
        }

        private NotificationTemplateSystem NotificationTemplateSystem { get; set; }

        public NotificationTemplateData RetrieveTemplateByType(NotificationTypes type)
        {
            return NotificationTemplateSystem.RetrieveTemplateByType(type);
        }
    }
}
