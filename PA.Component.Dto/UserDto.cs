using Framework.Core;

namespace PA.Component.Dto
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
