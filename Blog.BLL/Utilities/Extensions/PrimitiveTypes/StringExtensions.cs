using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.Utilities.Extensions.PrimitiveTypes
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
