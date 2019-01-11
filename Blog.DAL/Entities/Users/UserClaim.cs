using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Entities.Users
{
    public class UserClaim:IdentityUserClaim<Guid>
    {
    }
}
