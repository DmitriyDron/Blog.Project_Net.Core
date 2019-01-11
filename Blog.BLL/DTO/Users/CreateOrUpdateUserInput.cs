using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Users
{
    public class CreateOrUpdateUserInput
    {
        public UserDTO User { get; set; } = new UserDTO();

        public List<Guid> GrantedRoleIds { get; set; } = new List<Guid>();
    }
}