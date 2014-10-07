using System.Collections.Generic;
using Framework.Core;
using PA.Component.Dto;
using PA.Data;

namespace PA.Component.Converters
{
    public class UserConverter : IDataConverter<UserData, UserDto>
    {
        public IEnumerable<UserDto> Convert(IEnumerable<UserData> entitys)
        {
            List<UserDto> dtoList = new List<UserDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public UserDto Convert(UserData entity)
        {
            UserDto dto = new UserDto();

            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Username;
            dto.Email = entity.Email;
            dto.Username = entity.Username;
            dto.FullName = entity.FullName;

            return dto;
        }
    }
}
