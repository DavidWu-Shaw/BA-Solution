using Setup.Data;
using Framework.Service;
using Framework.UoW;

namespace Setup.Service.Contract
{
    public interface IUserService : IUpdateEntityService<UserData>
    {
        IServiceQueryResult<UserData> RetrieveByCredential(string email, string password);
        bool IsExists(string email);
    }
}
