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
        /// Searches for a certain number of Tag model records as a model (T),
        /// whose name contains a specific string.
        /// </ summary>
        /// <param name = "name"> String to search for records </ param>
        /// <param name = "numOfRecords"> Required number of entries </ param>
        /// <param name = "exp"> Expression to select Tag model fields </ param>
        /// <typeparam name = "T"> Model type with the required set of fields </ typeparam>
        /// <returns> Tag Model Entries as Model (T) </ returns>
        Task<IEnumerable<T>> FindByNameAsync<T>(string name, int numOfRecords, Expression<Func<Tag, T>> exp);
     
        /// <summary>
        /// Searches for a certain number of Tag model records as a model (T),
        /// whose name contains at least one of the specified lines.
        /// </ summary>
        /// <param name = "names"> Strings for searching for entries </ param>
        /// <param name = "exp"> Expression to select Tag model fields </ param>
        /// <typeparam name = "T"> Model type with the required set of fields </ typeparam>
        /// <returns> Tag Model Entries as Model (T) </ returns>
        Task<IEnumerable<T>> FindByNamesAsync<T>(IEnumerable<string> names, Expression<Func<Tag, T>> exp);
        void AddRange(IEnumerable<Tag> tags);
    }
}