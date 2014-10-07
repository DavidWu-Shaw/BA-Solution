using Framework.Core;

namespace PA.Component.Dto
{
    public class ProjectMemberDto : BaseDto
    {
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string AllowEdit { get; set; }
        public string AllowAdmin { get; set; }
    }
}
