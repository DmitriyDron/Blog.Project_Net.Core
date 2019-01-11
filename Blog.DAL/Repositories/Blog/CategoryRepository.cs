using Blog.DAL.Entities.Blog;
using Blog.DAL.EntityFramework;
using Blog.DAL.Extensions;
using Blog.DAL.Interfaces.Blog;
using Blog.DAL.Interfaces.UoW;
using Blog.DAL.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories.Blog
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        public IQueryable<Category> Query()
        {
            return _context.Set<Category>().AsQueryable();
        }

        public ICollection<Category> GetAll()
        {
            return _context.Set<Category>().ToList();
        }


        public async Task<ICollection<Category>> GetAllAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<Category, T>> exp)
        {
            exp.NotNull();
            return await _context.Categories
                .OrderBy(c => c.Name)
                .Select(exp)
                .ToListAsync();
        }

        public Category GetById(int id)
        {
            return _context.Set<Category>().Find(id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public Category GetByUniqueId(string id)
        {
            return _context.Set<Category>().Find(id);
        }

        public async Task<Category> GetByUniqueIdAsync(string id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public Category Find(Expression<Func<Category, bool>> match)
        {
            return _context.Set<Category>().SingleOrDefault(match);
        }

        public async Task<Category> FindAsync(Expression<Func<Category, bool>> match)
        {
            return await _context.Set<Category>().SingleOrDefaultAsync(match);
        }

        public ICollection<Category> FindAll(Expression<Func<Category, bool>> match)
        {
            return _context.Set<Category>().Where(match).ToList();
        }

        public async Task<ICollection<Category>> FindAllAsync(Expression<Func<Category, bool>> match)
        {
            return await _context.Set<Category>().Where(match).ToListAsync();
        }

        public Category Add(Category category)
        {
            _context.Set<Category>().Add(category);
            _context.SaveChanges();
            return category;
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Set<Category>().Add(category);
            await _unitOfWork.Commit();
            return category;
        }

        public Category Update(Category category)
        {
            category.NotNull();

            _context.Set<Category>().Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            category.NotNull();

            _context.Set<Category>().Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            await _unitOfWork.Commit();

            return category;
        }

        public void Delete(Category category)
        {
            _context.Set<Category>().Remove(category);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(Category category)
        {
            _context.Set<Category>().Remove(category);
            return await _unitOfWork.Commit();
        }

        public int Count()
        {
            return _context.Set<Category>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Category>().CountAsync();
        }

        public IEnumerable<Category> Filter(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<Category> query = _context.Set<Category>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public IQueryable<Category> FindBy(Expression<Func<Category, bool>> predicate)
        {
            return _context.Set<Category>().Where(predicate);
        }

        public bool Exist(Expression<Func<Category, bool>> predicate)
        {
            var exist = _context.Set<Category>().Where(predicate);
            return exist.Any() ? true : false;
        }
        public async Task<T> GetAsync<T>(int id, Expression<Func<Category, T>> exp)
        {
            exp.NotNull();
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(exp)
                .SingleOrDefaultAsync();
        }

       
        public async Task<bool> IsExistAsync(string name)
        {
            name.NotNull();
            return await _context.Categories.AnyAsync(t => t.Name == name);
        }
    }
}
 