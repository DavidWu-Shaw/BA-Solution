using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;

namespace CRM.Service
{
    public class LeadService : UpdateEntityService<ILeadRepository, LeadData>, ILeadService
    {
    }
}
