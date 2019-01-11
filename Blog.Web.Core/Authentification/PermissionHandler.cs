using Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Web.Core.Authentification
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permission;

        public PermissionHandler(IPermissionService permission)
        {
            _permission = permission;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var hasPermission = await _permission.IsUserGrantedToPermissionAsync(context.User.Identity.Name, requirement.Permission.Name);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}