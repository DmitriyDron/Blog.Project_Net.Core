using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Blog.BLL.DTO.Blog
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public CategoryDTO Category { get; set; }
        public DateTime? DateCreated { get; set; }
        public ICollection<TagDTO> Tags { get; set; }

        public PostDTO()
        {
            Tags = new Collection<TagDTO>();
        }
    }
}