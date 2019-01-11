using Blog.DAL.Entities.Blog;
using Blog.DAL.Entities.Blog.Query;
using Blog.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.Blog
{

    /// <summary>
    /// Содержит методы для работы с моделью Post
    /// </summary>
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<Post, T>> exp);
        Task<T> GetAsync<T>(int id, Expression<Func<Post, T>> exp);
        /// <summary>
        /// Читает выборку записей модели Post содержащей определенные поля.
        /// </summary>
        /// <param name="queryObj">Запрос для фильтрации записей</param>
        /// <param name="exp">Выражение для выборки полей модели Post</param>
        /// <typeparam name="T">Тип модели с необходимым набором полей</typeparam>
        /// <returns>Выборка записей модели Post в виде модели (T)</returns>
        Task<QueryResult<T>> GetQueryResultAsync<T>(PostQuery queryObj, Expression<Func<Post, T>> exp);
        /// <summary>
        /// Читает записи модели Tag в виде модели (T) c идентификатором связной модели Post.
        /// </summary>
        /// <param name="exp">Выражение для выборки полей модели Tag</param>
        /// <typeparam name="T">Тип модели с необходимым набором полей</typeparam>
        /// <returns>Выборка записей модели Tag c идентификатором связной модели Post.</returns>
        Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(Expression<Func<IdObject<Tag>, IdObject<T>>> exp);
        /// <summary>
        /// Читает записи модели Tag связанные с определенной записью модели Post.
        /// </summary>
        /// <param name="post">Идентификатор модели Post</param>
        /// <param name="exp">Выражение для выборки полей модели Tag</param>
        /// <typeparam name="T">Тип модели с необходимым набором полей</typeparam>
        /// <returns>Выборка записей модели Tag c идентификатором связной модели Post.</returns>
        Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(int post, Expression<Func<IdObject<Tag>, IdObject<T>>> exp);
        /// <summary>
        /// Читает записи модели Tag связанные с определенными записями модели Post.
        /// </summary>
        /// <param name="post">Список идентификаторов модели Post</param>
        /// <param name="exp">Выражение для выборки полей модели Tag</param>
        /// <typeparam name="T">Тип модели с необходимым набором полей</typeparam>
        /// <returns>Выборка записей модели Tag c идентификаторами связных моделей Post.</returns>
        Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(IEnumerable<int> posts, Expression<Func<IdObject<Tag>, IdObject<T>>> exp);
    }
}