using Blog.DAL.Entities.Blog;
using Blog.DAL.Entities.Blog.Query;
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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        public IQueryable<Post> Query()
        {
            return _context.Set<Post>().AsQueryable();
        }
        public async Task<T> GetAsync<T>(int id, Expression<Func<Post, T>> exp)
        {
            exp.NotNull();
            return await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Tags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => p.Id == id)
                .Select(exp)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<Post, T>> exp)
        {
            exp.NotNull();
            return await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Tags)
                    .ThenInclude(pt => pt.Tag)
                .Select(exp)
                .ToListAsync();
        }
        public ICollection<Post> GetAll()
        {
            return _context.Set<Post>().ToList();
        }

        public async Task<ICollection<Post>> GetAllAsync()
        {
            return await _context.Set<Post>().ToListAsync();
        }

        public Post GetById(int id)
        {
            return _context.Set<Post>().Find(id);
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Set<Post>().FindAsync(id);
        }

        public Post GetByUniqueId(string id)
        {
            return _context.Set<Post>().Find(id);
        }

        public async Task<Post> GetByUniqueIdAsync(string id)
        {
            return await _context.Set<Post>().FindAsync(id);
        }

        public Post Find(Expression<Func<Post, bool>> match)
        {
            return _context.Set<Post>().SingleOrDefault(match);
        }

        public async Task<Post> FindAsync(Expression<Func<Post, bool>> match)
        {
            return await _context.Set<Post>().SingleOrDefaultAsync(match);
        }

        public ICollection<Post> FindAll(Expression<Func<Post, bool>> match)
        {
            return _context.Set<Post>().Where(match).ToList();
        }

        public async Task<ICollection<Post>> FindAllAsync(Expression<Func<Post, bool>> match)
        {
            return await _context.Set<Post>().Where(match).ToListAsync();
        }

        public Post Add(Post post)
        {
            post.NotNull();
            _context.Set<Post>().Add(post);
            _context.SaveChanges();
            return post;
        }

        public async Task<Post> AddAsync(Post post)
        {
            post.NotNull();
            _context.Set<Post>().Add(post);
            await _unitOfWork.Commit();
            return post;
        }

        public Post Update(Post updated)
        {
            if (updated == null)
            {
                return null;
            }

            _context.Set<Post>().Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            _context.SaveChanges();

            return updated;
        }

        public async Task<Post> UpdateAsync(Post updated)
        {
            if (updated == null)
            {
                return null;
            }

            _context.Set<Post>().Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            await _unitOfWork.Commit();

            return updated;
        }

        public void Delete(Post post)
        {
            _context.Set<Post>().Remove(post);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(Post post)
        {
            _context.Set<Post>().Remove(post);
            return await _unitOfWork.Commit();
        }

        public int Count()
        {
            return _context.Set<Post>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Post>().CountAsync();
        }

        public IEnumerable<Post> Filter(Expression<Func<Post, bool>> filter = null, Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<Post> query = _context.Set<Post>();
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

        public IQueryable<Post> FindBy(Expression<Func<Post, bool>> predicate)
        {
            return _context.Set<Post>().Where(predicate);
        }

        public bool Exist(Expression<Func<Post, bool>> predicate)
        {
            var exist = _context.Set<Post>().Where(predicate);
            return exist.Any() ? true : false;
        }
        
        public async Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(Expression<Func<IdObject<Tag>, IdObject<T>>> exp)
        {
            exp.NotNull();
            return await GetTags(null, exp).ToListAsync();
        }

        public async Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(int post, Expression<Func<IdObject<Tag>, IdObject<T>>> exp)
        {
            exp.NotNull();
            return await GetTags(new[] { post }, exp).ToListAsync();
        }

        public async Task<IEnumerable<IdObject<T>>> GetTagsAsync<T>(IEnumerable<int> posts, Expression<Func<IdObject<Tag>, IdObject<T>>> exp)
        {
            exp.NotNull();
            posts.NotNull();
            return await GetTags(posts, exp).ToListAsync();
        }

        private IQueryable<IdObject<T>> GetTags<T>(IEnumerable<int> posts, Expression<Func<IdObject<Tag>, IdObject<T>>> exp)
        {
            exp.NotNull();
            var query = _context.PostTags
                .Include(pt => pt.Tag)
                .AsQueryable();

            if (posts != null && posts.Count() > 0)
                query = query.Where(pt => posts.Contains(pt.PostId));

            return query
                .Select(pt => new IdObject<Tag>() { Id = pt.PostId, Object = pt.Tag })
                .Select(exp);
        }

        public async Task<QueryResult<T>> GetQueryResultAsync<T>(PostQuery queryObj, Expression<Func<Post, T>> exp)
        {
            queryObj.NotNull();
            exp.NotNull();

            var result = new QueryResult<T>();

            var query = _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Tags)
                    .ThenInclude(pt => pt.Tag)
                .AsQueryable();

            query = query
                .ApplyFiltering(queryObj)
                .ApplySearching(queryObj);

            result.TotalItems = await query.CountAsync();

            query = query
                .ApplyOrdering(queryObj)
                .ApplyPaging(queryObj);

            result.Items = await query
                .Select(exp)
                .ToListAsync();

            return result;
        }

       
    }
}