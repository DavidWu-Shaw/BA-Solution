using Framework.Data;
using Setup.Data;

namespace Setup.Repository.Contract
{
    public interface IUserRepository : IUpdateEntityRepository<UserData>
    {
        UserData RetrieveByCredential(string email, string password);
        bool IsExists(string email);
    }
}
