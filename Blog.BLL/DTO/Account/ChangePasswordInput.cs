using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO.Account
{
    public class ChangePasswordInput
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string PasswordRepeat { get; set; }
    }
}
