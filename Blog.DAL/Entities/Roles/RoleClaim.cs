using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Entities.Roles
{
    public class RoleClaim:IdentityRoleClaim<Guid>
    {
    }
}
