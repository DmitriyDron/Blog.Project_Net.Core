using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Entities.Blog
{
    public class BlogModelState
    {
        public bool IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
    }
}