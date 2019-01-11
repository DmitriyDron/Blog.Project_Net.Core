using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Account
{
    public class RegisterInput
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}