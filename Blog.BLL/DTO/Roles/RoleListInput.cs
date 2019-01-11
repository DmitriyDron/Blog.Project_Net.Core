using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Roles
{
    public class RoleListInput : PagedListInput
    {
        public RoleListInput()
        {
            SortBy = "Name";
        }
    }
}