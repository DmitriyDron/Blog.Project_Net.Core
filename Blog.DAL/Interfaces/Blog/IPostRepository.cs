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
        
        /// Reads a selection of Post model records containing specific fields.
        /// </ summary>
        /// <param name = "queryObj"> Query to filter records </ param>
        /// <param name = "exp"> Expression to select fields of the Post model </ param>
        /// <typeparam name = "T"> Model type with the required set of fields </ typeparam>
        /// <returns> Fetching Post model entries as a model (T) </ returns>
        Task<QueryResult<T>> GetQueryResultAsync<T>(PostQuery queryObj, Expression<Func<Post, T>> exp);
       
        /// <summary>
        /// Reads the Tag model entries as a model (T) with the ID of the connected Post model.
        /// </ summary>
        /// <param name = "exp"> Expression to select Tag model fields </ param>
        /// <typeparam name = "T"> Model type with the required set of fields </ typeparam>
        /// <returns> Selection of records of the Tag model with the ID of the connected Post model. </ returns>
        Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(Expression<Func<IdObject<Tag>, IdObject<T>>> exp);
       
        /// <summary>
        /// Reads the Tag model entries associated with a particular Post model entry.
        /// </ summary>
        /// <param name = "post"> Post Model ID </ param>
        /// <param name = "exp"> Expression to select Tag model fields </ param>
        /// <typeparam name = "T"> Model type with the required set of fields </ typeparam>
        /// <returns> Selection of records of the Tag model with the ID of the connected Post model. </ returns>
        Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(int post, Expression<Func<IdObject<Tag>, IdObject<T>>> exp);
       
        /// <summary>
        /// Reads Tag model entries associated with specific Post model entries.
        /// </ summary>
        /// <param name = "post"> Post model identifier list </ param>
        /// <param name = "exp"> Expression to select Tag model fields </ param>
        /// <typeparam name = "T"> Model type with the required set of fields </ typeparam>
        /// <returns> Selection of Tag model records with identifiers of Post connected models. </ returns>
        Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(IEnumerable<int> posts, Expression<Func<IdObject<Tag>, IdObject<T>>> exp);
    }
}