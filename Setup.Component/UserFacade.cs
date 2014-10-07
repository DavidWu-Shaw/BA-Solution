using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Data;

namespace Setup.Component
{
    public class UserFacade : BusinessComponent
    {
        public UserFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            UserSystem = new UserSystem(unitOfWork);
        }

        private UserSystem UserSystem { get; set; }

        public IEnumerable<UserDto> RetrieveAllUser()
        {
            IEnumerable<UserDto> instances = UserSystem.RetrieveAllUser(new UserConverter());
            if (instances == null)
            {
                instances = new List<UserDto>();
            }
            return instances;
        }

        public UserDto RetrieveOrNewUser(object id)
        {
            return UserSystem.RetrieveOrNewUser(id, new UserConverter());
        }

        public IFacadeUpdateResult<UserData> SaveUser(UserDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<UserData> result = UserSystem.SaveUser(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IFacadeUpdateResult<UserData> DeleteUser(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<UserData> result = UserSystem.DeleteUser(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }
            return result;
        }

        public IList<BindingListItem> GetBindingList()
        {
            return UserSystem.GetBindingList();
        }
    }
}
