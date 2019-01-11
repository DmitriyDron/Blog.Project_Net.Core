using Blog.DAL.Entities.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.Core.Controllers
{
    [Authorize(Policy = DefaultPermissions.PermissionNameForAdministration)]
    public class AdminController : BaseController
    {

    }
}

