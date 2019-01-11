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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        public IQueryable<Tag> Query()
        {
            return _context.Set<Tag> ().AsQueryable();
        }

        public ICollection<Tag> GetAll()
        {
            return _context.Set<Tag>().ToList();
        }

        public async Task<ICollection<Tag>> GetAllAsync()
        {
            return await _context.Set<Tag>().ToListAsync();
        }

        public Tag GetById(int id)
        {
            return _context.Set<Tag>().Find(id);
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Set<Tag>().FindAsync(id);
        }

        public Tag GetByUniqueId(string id)
        {
            return _context.Set<Tag>().Find(id);
        }

        public async Task<Tag> GetByUniqueIdAsync(string id)
        {
            return await _context.Set<Tag>().FindAsync(id);
        }

        public Tag Find(Expression<Func<Tag, bool>> match)
        {
            return _context.Set<Tag>().SingleOrDefault(match);
        }

        public async Task<Tag> FindAsync(Expression<Func<Tag, bool>> match)
        {
            return await _context.Set<Tag>().SingleOrDefaultAsync(match);
        }

        public ICollection<Tag> FindAll(Expression<Func<Tag, bool>> match)
        {
            return _context.Set<Tag>().Where(match).ToList();
        }

        public async Task<ICollection<Tag>> FindAllAsync(Expression<Func<Tag, bool>> match)
        {
            return await _context.Set<Tag>().Where(match).ToListAsync();
        }

        public Tag Add(Tag tag)
        {
            _context.Set<Tag>().Add(tag);
            _context.SaveChanges();
            return tag;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            tag.NotNull();
            _context.Set<Tag>().Add(tag);
            await _unitOfWork.Commit();
            return tag;
        }

        public Tag Update(Tag updated)
        {
            if (updated == null)
            {
                return null;
            }

            _context.Set<Tag>().Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            _context.SaveChanges();

            return updated;
        }

        public async Task<Tag> UpdateAsync(Tag updated)
        {
            if (updated == null)
            {
                return null;
            }

            _context.Set<Tag>().Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            await _unitOfWork.Commit();

            return updated;
        }

        public void Delete(Tag tag)
        {
            tag.NotNull();
            _context.Set<Tag>().Remove(tag);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(Tag tag)
        {
            tag.NotNull();
            _context.Set<Tag>().Remove(tag);
            return await _unitOfWork.Commit();
        }

        public int Count()
        {
            return _context.Set<Tag>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Tag>().CountAsync();
        }

        public IEnumerable<Tag> Filter(Expression<Func<Tag, bool>> filter = null, Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<Tag> query = _context.Set<Tag>();
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

        public IQueryable<Tag> FindBy(Expression<Func<Tag, bool>> predicate)
        {
            return _context.Set<Tag>().Where(predicate);
        }

        public bool Exist(Expression<Func<Tag, bool>> predicate)
        {
            var exist = _context.Set<Tag>().Where(predicate);
            return exist.Any() ? true : false;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<Tag, T>> exp)
        {
            exp.NotNull();
            return await _context.Tags.Select(exp).ToListAsync();
        }

        public async Task<T> GetAsync<T>(int id, Expression<Func<Tag, T>> exp)
        {
            exp.NotNull();
            return await _context.Tags.Where(c => c.Id == id).Select(exp).SingleOrDefaultAsync();
        }


      
        public async Task<IEnumerable<T>> FindByNameAsync<T>(string name, int numOfRecords, Expression<Func<Tag, T>> exp)
        {
            name.NotNull();
            exp.NotNull();

            var query = _context.Tags
                .Where(t => t.Name.Contains(name))
                .OrderBy(t => t.Name)
                .AsQueryable();

            if (numOfRecords > 0)
                query = query.Take(numOfRecords);

            return await query
                .Select(exp)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByNamesAsync<T>(IEnumerable<string> names, Expression<Func<Tag, T>> exp)
        {
            names.NotNull();
            exp.NotNull();

            return await _context.Tags
                .Where(t => names.Contains(t.Name))
                .Select(exp)
                .ToListAsync();
        }

        public void AddRange(IEnumerable<Tag> tags)
        {
            tags.NotNull();
            _context.Tags.AddRange(tags);
        }
    }
}