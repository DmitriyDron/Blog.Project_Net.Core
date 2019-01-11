using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.DAL.Entities.Blog
{

    public class Category : BlogModelState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public  virtual ICollection<Post> Posts { get; set; }

        public Category()
        {
            Posts = new Collection<Post>();
        }
    }
}