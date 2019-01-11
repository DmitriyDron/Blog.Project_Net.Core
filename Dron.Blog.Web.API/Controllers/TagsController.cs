using Blog.BLL.DTO.Blog;
using Blog.BLL.Interfaces.Blog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Core.Controllers;

namespace Blog.Web.API.Controllers
{
  
    public class TagsController:BaseController
    {
        private readonly ITagService tagService;

        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<IEnumerable<TagDTO>> GetTags(string name, int records)
        {
            return await tagService.FindFirsTagsLike(name, records);
        }
    }
}