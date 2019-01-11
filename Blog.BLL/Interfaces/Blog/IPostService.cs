using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Query;
using Blog.BLL.DTO.Blog.Save;
using Blog.DAL.Entities.Blog;
using Blog.DAL.Entities.Blog.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces.Blog
{
    /// <summary>
    /// Содержит методы для обслуживания модели Post
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Читает все записи модели Post из БД в виде модели представления PostViewModel.
        /// </summary>
        /// <returns>Все записи из БД модели Post в виде модели представления PostViewModel</returns>
        Task<IEnumerable<PostDTO>> GetAllViewModelsAsync();
        /// <summary>
        /// Читает выборку записей модели Post из БД в виде модели представления PostViewModel.
        /// Модель представления содержит только поля необходимы для отображения блога.
        /// </summary>
        /// <param name="queryViewModel">Запрос для фильтрации записей</param>
        /// <returns>Выборка записей из БД модели Post в виде модели представления PostViewModel</returns>
        Task<QueryResult<PostDTO>> GetBlogViewModels(PostQueryDTO queryViewModel);
        /// <summary>
        /// Читает выборку записей модели Post из БД в виде модели представления PostViewModel.
        /// Модель представления содержит только поля необходимы для администрирования блога.
        /// </summary>
        /// <param name="queryViewModel">Запрос для фильтрации записей</param>
        /// <returns>Выборка записей из БД модели Post в виде модели представления PostViewModel</returns>
        Task<QueryResult<PostDTO>> GetAdminViewModels(PostQueryDTO queryViewModel);
        /// <summary>
        /// Читает определенную запись модели Post из БД в виде модели представления PostViewModel.
        /// Модель представления содержит только поля необходимы для отображения статьи.
        /// </summary>
        /// <param name="id">Id записи модели Post</param>
        /// <returns>Запись модели Post в виде модели представления PostViewModel</returns>
        Task<PostDTO> GetPostViewModel(int id);
        /// <summary>
        /// Создает модели Post в соответствии с моделью представления SavePostViewModel и списком тегов.
        /// </summary>
        /// <param name="savePost">Модель представления SavePostViewModel, содержащая данные для создания модели Post</param>
        /// <param name="tags">Список тегов для добавления в модель Post</param>
        /// <returns>Модель, которая будет создана в БД после вызова SaveAsync</returns>
        Post AddPost(SavePostDTO savePost, IEnumerable<Tag> tags);
        /// <summary>
        /// Изменяет модель Post в соответствии с моделью представления SavePostViewModel
        /// и списком тегов.
        /// </summary>
        /// <param name="savePost">Модель представления SavePostViewModel, содержащая данные для изменения модели Post</param>
        /// <param name="tags">Новый список тегов для добавления в модель Post</param>
        /// <param name="id">Id записи модели Post</param>
        /// <returns>Модель, которая будет записана в БД после вызова SaveAsync</returns>
        Task<Post> UpdatePost(int id, SavePostDTO savePost, IEnumerable<Tag> tags);
        /// <summary>
        /// Изменяет данные модели Post если предоставлен идентификатор записи, 
        /// иначе создает новую в соответствии с моделью представления SavePostViewModel.
        /// </summary>
        /// <param name="savePost">Модель представления SavePostViewModel, содержащая данные для изменения или создания модели Post</param>
        /// <param name="tags">Новый список тегов для модели Post</param>
        /// <param name="id">nullable Id записи модели Post</param>
        /// <returns>Модель, которая будет записана или изменена в БД после вызова SaveAsync</returns>
        Task<Post> UpdateOrAddPostIfIdIsNull(int? id, SavePostDTO savePost, IEnumerable<Tag> tags);
        /// <summary>
        /// Помечает запись как удаленную.
        /// </summary>
        /// <param name="id">Id записи модели Post</param>
        void Remove(int id);
    }
}