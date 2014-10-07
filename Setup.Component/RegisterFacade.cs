using System;
using Framework.Component;
using Framework.Core;
using Framework.Security;
using Framework.UoW;
using Framework.Validation;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Core;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    public class RegisterFacade : BusinessComponent
    {
        public RegisterFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IFacadeUpdateResult<UserData> RegisterUser(UserDto dto)
        {
            ArgumentValidator.IsNotNull("data", dto);

            UnitOfWork.BeginTransaction();

            FacadeUpdateResult<UserData> result = new FacadeUpdateResult<UserData>();
            IUserService service = UnitOfWork.GetService<IUserService>();

            ValidateEmail(service, dto, result.ValidationResult);

            if (result.IsSuccessful)
            {
                User instance = service.CreateNew<User>();
                instance.Email = dto.Email;
                instance.Username = dto.Username;
                if (dto.Password.HasValue())
                {
                    instance.Password = EncryptionHelper.Decrypt(dto.Password);
                }
                instance.DomainId = (int)UserDomains.Member;
                instance.CreatedDate = DateTime.Today;
                instance.ModifiedDate = DateTime.Today;
                instance.LastConnectDate = DateTime.Now;
                instance.IsActive = true;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<UserData>());
                result.Merge(saveQuery);
            }

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

        private void ValidateEmail(IUserService service, UserDto dto, ValidationResult result)
        {
            if (service.IsExists(dto.Email))
            {
                result.AddItem(typeof(User),
                    BuildLocalizationKey("EmailUsed"),
                    ValidationItemType.Error,
                    Localize("EmailUsed"),
                    "Email");
            }
        }
    }
}
