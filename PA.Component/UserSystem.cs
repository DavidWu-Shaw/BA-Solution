using System;
using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using PA.Business;
using PA.Component.Dto;
using PA.Data;
using PA.Service.Contract;

namespace PA.Component
{
    internal class UserSystem : BusinessComponent
    {
        public UserSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAll<TDto>(IDataConverter<UserData, TDto> converter)
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

        internal TDto RetrieveOrNewProject<TDto>(object projectId, IDataConverter<ProjectData, TDto> converter)
    where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IProjectService service = UnitOfWork.GetService<IProjectService>();
            FacadeUpdateResult<ProjectData> result = new FacadeUpdateResult<ProjectData>();
            Project project = RetrieveOrNew<ProjectData, Project, IProjectService>(result.ValidationResult, projectId);
            if (result.IsSuccessful)
            {
                return converter.Convert(project.RetrieveData<ProjectData>());
            }
            else
            {
                return null;
            }
        }


        internal TDto RetrieveUser<TDto>(object id, IDataConverter<UserData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IUserService service = UnitOfWork.GetService<IUserService>();
            FacadeUpdateResult<UserData> result = new FacadeUpdateResult<UserData>();
            User user = RetrieveOrNew<UserData, User, IUserService>(result.ValidationResult, id);
            if (result.IsSuccessful)
            {
                return converter.Convert(user.RetrieveData<UserData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<UserData> SaveUser(UserDto userDto)
        {
            ArgumentValidator.IsNotNull("userDto", userDto);

            IUserService service = UnitOfWork.GetService<IUserService>();

            FacadeUpdateResult<UserData> result = new FacadeUpdateResult<UserData>();

            if (userDto.Email.TrimHasValue()
                && !service.IsUnique(userDto.Email, userDto.Id))
            {
                AddError(result.ValidationResult, "DuplicateUserEmail");
                return result;
            }

            //if (userDto.Password != null)
            //{
            //    SecurityPolicyService policyService = new SecurityPolicyService(Configuration);

            //    ValidationResult policyValidationResult = policyService.ValidateCredential(userDto.Email, userDto.Password, Configuration.PasswordPolicies);

            //    result.ValidationResult.Merge(policyValidationResult);

            //    if (!result.IsSuccessful)
            //    {
            //        return result;
            //    }
            //}

            User user = RetrieveOrNew<UserData, User, IUserService>(result.ValidationResult, userDto.Id);

            if (result.IsSuccessful)
            {
                user.Username = userDto.Username;
                user.Email = userDto.Email;
                user.FullName = userDto.FullName;

                if (userDto.Password.HasValue())
                {
                    user.Password = userDto.Password;
                }

                var saveQuery = user.Save();

                result.AttachResult(user.RetrieveData<UserData>());
                result.Merge(saveQuery);
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
                foreach (UserData user in query.DataList)
                {
                    dataSource.Add(new BindingListItem(user.Id, user.Username));
                }
            }

            return dataSource;
        }
    }
}
