using System.Collections.Generic;
using Framework.Component;
using Framework.UoW;
using PA.Component.Converters;
using PA.Component.Dto;

namespace PA.Component
{
    public class UserFacade : BusinessComponent
    {
        public UserFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            UserSystem = new UserSystem(unitOfWork);
        }

        private UserSystem UserSystem { get; set; }

        public IEnumerable<UserDto> RetrieveAll()
        {
            return UserSystem.RetrieveAll(new UserConverter());
        }

        public UserDto RetrieveUser(object id)
        {
            return UserSystem.RetrieveUser(id, new UserConverter());
        }

    }
}
