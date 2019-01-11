using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.DAL.Entities.Blog
{
    public class Tag : BlogModelState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public  virtual ICollection<PostTag> Posts { get; set; }

        public Tag()
        {
            Posts = new Collection<PostTag>();
        }
    }
}