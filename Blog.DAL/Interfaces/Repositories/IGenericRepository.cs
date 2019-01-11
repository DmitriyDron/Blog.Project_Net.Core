using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();

        ICollection<TEntity> GetAll();

        Task<ICollection<TEntity>> GetAllAsync();

       

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        TEntity GetByUniqueId(string id);

        Task<TEntity> GetByUniqueIdAsync(string id);

        TEntity Find(Expression<Func<TEntity, bool>> match);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity updated);

        Task<TEntity> UpdateAsync(TEntity updated);

        void Delete(TEntity t);

        Task<int> DeleteAsync(TEntity t);

        int Count();

        Task<int> CountAsync();

        IEnumerable<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        bool Exist(Expression<Func<TEntity, bool>> predicate);
    }
}