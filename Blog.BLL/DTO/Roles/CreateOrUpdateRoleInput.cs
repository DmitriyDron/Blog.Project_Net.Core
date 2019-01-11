using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Roles
{
    public class CreateOrUpdateRoleInput
    {
        public RoleDTO Role { get; set; } = new RoleDTO();

        public List<Guid> GrantedPermissionIds { get; set; } = new List<Guid>();
    }
}