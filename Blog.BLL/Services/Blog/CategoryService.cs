using AutoMapper;
using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Save;
using Blog.BLL.Interfaces.Blog;
using Blog.DAL.Entities.Blog;
using Blog.DAL.Extensions;
using Blog.DAL.Interfaces.Blog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.BLL.Services.Blog
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllViewModelsAsync()
        {
            return await categoryRepository.GetAllAsync(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<Category> AddIfNotExists(SaveCategoryDTO saveCategory)
        {
            saveCategory.NotNull();

            if (await categoryRepository.IsExistAsync(saveCategory.Name))
                return null;

            var dateCreated = DateTime.Now;
            var category = mapper.Map<SaveCategoryDTO, Category>(saveCategory);
            category.IsActive = true;
            category.DateCreated = dateCreated;
            category.DateLastUpdated = dateCreated;

            categoryRepository.Add(category);

            return category;
        }

        public void Remove(int id)
        {
            categoryRepository.Delete(new Category() { Id = id });
        }
    }
}