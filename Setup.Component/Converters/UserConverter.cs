using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Core;
using Setup.Data;
using Setup.Component.Dto;

namespace Setup.Component.Converters
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
            dto.Username = entity.Username;
            dto.Email = entity.Email;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
            dto.LastConnectDate = entity.LastConnectDate;
            dto.DomainId = entity.DomainId;
            dto.IsActive = entity.IsActive;
            dto.LanguageId = entity.LanguageId;
            dto.MatchId = entity.MatchId;
            dto.FullName = entity.FullName;

            return dto;
        }
    }
}
