using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Account
{
    public class LoginInput
    {
        public string UserNameOrEmail { get; set; }

        public string Password { get; set; }
    }
}