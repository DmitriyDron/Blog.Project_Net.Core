using Blog.DAL.Entities.Blog;
using Blog.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.Blog
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<Category, T>> exp);
        /// <summary>
        /// Проверяет существует ли определенная запись модели Category в БД.
        /// </summary>
        /// <param name="name">Строка для поиска записи</param>
        /// <returns>ture eсли запись существует, иначе false</returns>
        Task<bool> IsExistAsync(string name);
    }
}