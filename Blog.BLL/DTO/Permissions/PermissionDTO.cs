using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Permissions
{
    public class PermissionDTO : EntityDTO
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}
