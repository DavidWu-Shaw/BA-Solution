using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class UserSystem : BusinessComponent
    {
        public UserSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllUser<TDto>(IDataConverter<UserData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IUserService service = UnitOfWork.GetService<IUserService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewUser<TDto>(object instanceId, IDataConverter<UserData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IUserService service = UnitOfWork.GetService<IUserService>();
            FacadeUpdateResult<UserData> result = new FacadeUpdateResult<UserData>();
            User instance = RetrieveOrNew<UserData, User, IUserService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<UserData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<UserData> SaveUser(UserDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<UserData> result = new FacadeUpdateResult<UserData>();
            IUserService service = UnitOfWork.GetService<IUserService>();
            User instance = RetrieveOrNew<UserData, User, IUserService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Username = dto.Username;
                instance.Email = dto.Email;
                instance.IsActive = dto.IsActive;
                instance.CreatedDate = dto.CreatedDate;
                instance.ModifiedDate = dto.ModifiedDate;
                instance.LastConnectDate = dto.LastConnectDate;
                instance.DomainId = dto.DomainId;
                instance.LanguageId = dto.LanguageId;
                instance.MatchId = dto.MatchId;
                instance.FullName = dto.FullName;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<UserData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<UserData> DeleteUser(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<UserData> result = new FacadeUpdateResult<UserData>();
            IUserService service = UnitOfWork.GetService<IUserService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                User instance = query.ToBo<User>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "UserCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IUserService service = UnitOfWork.GetService<IUserService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (UserData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Username));
                }
            }

            return dataSource;
        }
    }
}
