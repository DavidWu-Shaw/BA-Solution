using Framework.Service;
using Framework.UoW;
using Setup.Data;

namespace Setup.Service.Contract
{
    public interface IAdministratorService : IUpdateEntityService<AdministratorData>
    {
        IServiceQueryResult<AdministratorData> RetrieveByCredential(string username, string password);
        bool IsExists(string email);
    }
}
