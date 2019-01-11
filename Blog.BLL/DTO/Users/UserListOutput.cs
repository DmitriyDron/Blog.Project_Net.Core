using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Users
{
    public class UserListOutput : PagedListOutput
    {
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
