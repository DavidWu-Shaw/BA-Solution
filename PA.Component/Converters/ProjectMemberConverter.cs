using System.Collections.Generic;
using Framework.Core;
using Framework.UoW;
using PA.Business;
using PA.Component.Dto;

namespace PA.Component.Converters
{
    public class ProjectMemberConverter : IBusinessObjectConverter<ProjectMember, ProjectMemberDto>
    {
        public IEnumerable<ProjectMemberDto> Convert(IEnumerable<ProjectMember> entitys)
        {
            List<ProjectMemberDto> dtoList = new List<ProjectMemberDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public ProjectMemberDto Convert(ProjectMember entity)
        {
            ProjectMemberDto dto = new ProjectMemberDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.MemberName;
            dto.MemberName = entity.MemberName;
            dto.MemberEmail = entity.MemberEmail;
            dto.AllowEdit = entity.AllowEdit.ToString();
            dto.AllowAdmin = entity.AllowAdmin.ToString();

            return dto;
        }
    }
}
