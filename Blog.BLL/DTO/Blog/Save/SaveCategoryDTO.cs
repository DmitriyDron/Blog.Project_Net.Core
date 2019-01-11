using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.BLL.DTO.Blog.Save
{
    public class SaveCategoryDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
