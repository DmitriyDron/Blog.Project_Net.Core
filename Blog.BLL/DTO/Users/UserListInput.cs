using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Users
{
    public class UserListInput : PagedListInput
    {
        public UserListInput()
        {
            SortBy = "UserName";
        }
    }
}