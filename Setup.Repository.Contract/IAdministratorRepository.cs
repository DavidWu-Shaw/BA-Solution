using Framework.Data;
using Setup.Data;

namespace Setup.Repository.Contract
{
    public interface IAdministratorRepository : IUpdateEntityRepository<AdministratorData>
    {
        AdministratorData RetrieveByCredential(string email, string password);
        bool IsExists(string email);
    }
}
