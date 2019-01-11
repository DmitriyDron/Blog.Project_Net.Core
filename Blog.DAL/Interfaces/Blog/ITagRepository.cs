using Blog.DAL.Entities.Blog;
using Blog.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.Blog
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        /// <summary>
        /// Ищет определенное количество записей модели Tag в виде модели (T), 
        /// имя которых содержит определённую строку.
        /// </summary>
        /// <param name="name">Строка для поиска записей</param>
        /// <param name="numOfRecords">Необходимое количество записей</param>
        /// <param name="exp">Выражение для выборки полей модели Tag</param>
        /// <typeparam name="T">Тип модели с необходимым набором полей</typeparam>
        /// <returns>Записи модели Tag в виде модели (T)</returns>
        Task<IEnumerable<T>> FindByNameAsync<T>(string name, int numOfRecords, Expression<Func<Tag, T>> exp);
        /// <summary>
        /// Ищет определенное количество записей модели Tag  в виде модели (T), 
        /// имя которых содержит хотя бы одну из определенных строк.
        /// </summary>
        /// <param name="names">Строки для поиска записей</param>
        /// <param name="exp">Выражение для выборки полей модели Tag</param>
        /// <typeparam name="T">Тип модели с необходимым набором полей</typeparam>
        /// <returns>Записи модели Tag в виде модели (T)</returns>
        Task<IEnumerable<T>> FindByNamesAsync<T>(IEnumerable<string> names, Expression<Func<Tag, T>> exp);
        void AddRange(IEnumerable<Tag> tags);
    }
}