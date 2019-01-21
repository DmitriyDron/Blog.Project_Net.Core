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
        /// Checks if a specific entry of Category Category exists in the database.
        /// </ summary>
        /// <param name = "name"> The string to search for an entry </ param>
        /// <returns> ture if the record exists, otherwise false </ returns>
        Task<bool> IsExistAsync(string name);
    }
}