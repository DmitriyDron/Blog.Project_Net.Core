using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Interfaces.Blog
{
    public class IdObject<T>
    {
        public int Id { get; set; }
        public T Object { get; set; }
    }
}