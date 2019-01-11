using Blog.BLL.DTO.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Roles
{
    public class GetRoleForCreateOrUpdateOutput
    {
        public RoleDTO Role { get; set; } = new RoleDTO();

        public List<PermissionDTO> AllPermissions { get; set; } = new List<PermissionDTO>();

        public List<Guid> GrantedPermissionIds { get; set; } = new List<Guid>();
    }
}