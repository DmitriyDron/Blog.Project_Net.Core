using Blog.DAL.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Entities.Roles
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
