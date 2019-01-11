
using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Query;
using Blog.BLL.DTO.Blog.Save;
using Blog.BLL.Interfaces;
using Blog.BLL.Interfaces.Blog;
using Blog.DAL.Entities.Blog.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Core.Controllers;

namespace Blog.Web.API.Controllers
{
   
    public class PostsController : BaseController
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly IPostService postService;
        private readonly ITagService tagService;

        public PostsController(IUnitOfWorkService unitOfWork, IPostService postService, ITagService tagService)
        {
            this.unitOfWork = unitOfWork;
            this.postService = postService;
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDTO>> GetPosts()
        {
            return await postService.GetAllViewModelsAsync();
        }

        [HttpGet("query")]
        public async Task<QueryResult<PostDTO>> GetPostsQuery(PostQueryDTO query)
        {
            return await postService.GetBlogViewModels(query);
        }

        [HttpGet("admin")]
        public async Task<QueryResult<PostDTO>> GetAdminQuery(PostQueryDTO query)
        {
            return await postService.GetAdminViewModels(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var postViewModel = await postService.GetPostViewModel(id);
            if (postViewModel == null)
                return NotFound();
            return Ok(postViewModel);
        }

        [HttpPut("{id}")]
     //   [Authorize]
        public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] SavePostDTO savePost)
        {
            return await UpdateOrCreate(id, savePost);
        }

        [HttpPost]
     //   [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] SavePostDTO savePost)
        {
            return await UpdateOrCreate(null, savePost);
        }

        [HttpDelete("{id}")]
     //   [Authorize]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            postService.Remove(id);

            if (!await unitOfWork.TrySaveChangesAsync())
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(id);
        }

        private async Task<IActionResult> UpdateOrCreate(int? id, SavePostDTO savePost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tags = await tagService.FindByNamesAndAddIfNotExists(savePost.Tags);
            var post = await postService.UpdateOrAddPostIfIdIsNull(id, savePost, tags);

            if (post == null)
                return NotFound();

            if (!await unitOfWork.TrySaveChangesAsync())
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(post.Id);
        }
    }
}