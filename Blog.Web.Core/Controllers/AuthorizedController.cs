using Blog.DAL.Entities.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.Core.Controllers
{
    [Authorize(Policy = DefaultPermissions.PermissionNameForMemberAccess)]
    public class AuthorizedController : BaseController
    {

    }
}