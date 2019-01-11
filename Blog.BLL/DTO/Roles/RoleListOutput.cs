using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Roles
{
    public class RoleListOutput : PagedListOutput
    {
        public string Name { get; set; }

        public bool IsSystemDefault { get; set; }
    }
}