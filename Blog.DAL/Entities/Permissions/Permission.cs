using Blog.DAL.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
