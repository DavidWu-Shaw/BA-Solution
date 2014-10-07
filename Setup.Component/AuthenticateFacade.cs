using System;
using Framework.Component;
using Framework.Security;
using Framework.UoW;
using Setup.Business;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    public class AuthenticateFacade : BusinessComponent
    {
        public AuthenticateFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public UserIdentity Authenticate(string email, string encryptedPassword)
        {
            UserIdentity authenticatedUser = null;
            IUserService service = UnitOfWork.GetService<IUserService>();

            string password = EncryptionHelper.Decrypt(encryptedPassword);
            IServiceQueryResult<UserData> query = service.RetrieveByCredential(email, password);

            if (query.HasResult)
            {
                authenticatedUser = new UserIdentity(query.Data);

                User user = query.ToBo<User>();
                user.LastConnectDate = DateTime.Now;
                user.Save();
            }

            return authenticatedUser;
        }

        public UserIdentity AuthenticateAdministrator(string username, string password)
        {
            UserIdentity authenticatedUser = null;
            IAdministratorService service = UnitOfWork.GetService<IAdministratorService>();
            IServiceQueryResult<AdministratorData> query = service.RetrieveByCredential(username, password);

            if (query.HasResult)
            {
                authenticatedUser = new UserIdentity(query.Data);

                Administrator user = query.ToBo<Administrator>();
                user.LastConnectDate = DateTime.Now;
                user.Save();
            }

            return authenticatedUser;
        }
    }
}
