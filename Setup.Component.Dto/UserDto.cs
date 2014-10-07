using System;
using Framework.Core;

namespace Setup.Component.Dto
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? LastConnectDate { get; set; }
        public int DomainId { get; set; }
        public bool IsActive { get; set; }
        public object LanguageId { get; set; }
        public object MatchId { get; set; }
        public string FullName { get; set; }
    }
}
