using Blog.BLL.DTO.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Users
{
    public class GetUserForCreateOrUpdateOutput
    {
        public UserDTO User { get; set; } = new UserDTO();

        public List<RoleDTO> AllRoles { get; set; } = new List<RoleDTO>();

        public List<Guid> GrantedRoleIds { get; set; } = new List<Guid>();
    }
}