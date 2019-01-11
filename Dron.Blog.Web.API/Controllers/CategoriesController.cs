using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Save;
using Blog.BLL.Interfaces;
using Blog.BLL.Interfaces.Blog;
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
 
    public class CategoriesController : BaseController
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly ICategoryService categoryService;

        public CategoriesController(IUnitOfWorkService unitOfWork, ICategoryService categoryService)
        {
            this.unitOfWork = unitOfWork;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return await categoryService.GetAllViewModelsAsync();
        }

        [HttpPost]
      //  [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] SaveCategoryDTO saveCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await categoryService.AddIfNotExists(saveCategory);
            if (category == null)
            {
                ModelState.AddModelError("", $"{saveCategory.Name} category already exists");
                return BadRequest(ModelState);
            }

            if (!await unitOfWork.TrySaveChangesAsync())
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(category.Id);
        }

        [HttpDelete("{id}")]
      //  [Authorize]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            categoryService.Remove(id);

            if (!await unitOfWork.TrySaveChangesAsync())
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(id);
        }
    }
}