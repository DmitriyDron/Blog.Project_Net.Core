using Blog.DAL.Entities.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.Core.Authentification
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}