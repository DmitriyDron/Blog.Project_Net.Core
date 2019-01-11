using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Save;
using Blog.DAL.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces.Blog
{
    /// <summary>
    /// Содержит методы для обслуживания модели Category
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Читает все записи модели Category из БД в виде модели представления CategoryViewModel.
        /// </summary>
        /// <returns>Все записи из БД модели Post в виде PostViewModel</returns>
        Task<IEnumerable<CategoryDTO>> GetAllViewModelsAsync();
        /// <summary>
        /// Если БД не содержит данную модель Category, 
        /// то создает новую в соответствии с моделью представления SaveCategoryViewModel.
        /// </summary>
        /// <param name="saveCategory">Модель представления SaveCategoryViewModel, содержащая данные для создания модели Category</param>
        /// <returns>Модель, которая будет создана в БД после вызова SaveAsync</returns>
        Task<Category> AddIfNotExists(SaveCategoryDTO saveCategory);
        /// <summary>
        /// Помечает запись как удаленную.
        /// </summary>
        /// <param name="id">Id записи модели Category</param>
        void Remove(int id);
    }
}