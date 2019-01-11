using AutoMapper;
using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Query;
using Blog.BLL.DTO.Blog.Save;
using Blog.BLL.Interfaces.Blog;
using Blog.DAL.Entities.Blog;
using Blog.DAL.Entities.Blog.Query;
using Blog.DAL.Extensions;
using Blog.DAL.Interfaces.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services.Blog
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        private Expression<Func<IdObject<Tag>, IdObject<TagDTO>>> tagExp;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;

            tagExp = (IdObject<Tag> idTag) => new IdObject<TagDTO>()
            {
                Id = idTag.Id,
                Object = new TagDTO()
                {
                    Id = idTag.Object.Id,
                    Name = idTag.Object.Name
                }
            };
        }

        public async Task<IEnumerable<PostDTO>> GetAllViewModelsAsync()
        {
            var tags = await postRepository.GetTagsAsync(tagExp);
            return await postRepository.GetAllAsync((Post p) => new PostDTO()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ShortContent = p.ShortContent,
                Category = new CategoryDTO()
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                },
                DateCreated = p.DateCreated,
                Tags = tags.Where(pt => pt.Id == p.Id).Select(pt => pt.Object).ToList()
            });
        }

        public async Task<QueryResult<PostDTO>> GetBlogViewModels(PostQueryDTO queryViewModel)
        {
            queryViewModel.NotNull();

            var query = mapper.Map<PostQueryDTO, PostQuery>(queryViewModel);
            var result = await postRepository.GetQueryResultAsync(query, (Post p) => new PostDTO()
            {
                Id = p.Id,
                Title = p.Title,
                ShortContent = p.ShortContent,
                Category = new CategoryDTO()
                {
                    Name = p.Category.Name
                },
                DateCreated = p.DateCreated,
            });
            var tags = await postRepository.GetTagsAsync(result.Items.Select(p => p.Id), tagExp);
            foreach (var p in result.Items)
                p.Tags = tags.Where(t => t.Id == p.Id).Select(t => t.Object).ToList();

            return result;
        }

        public async Task<QueryResult<PostDTO>> GetAdminViewModels(PostQueryDTO queryViewModel)
        {
            queryViewModel.NotNull();

            var query = mapper.Map<PostQueryDTO, PostQuery>(queryViewModel);
            var result = await postRepository.GetQueryResultAsync(query, (Post p) => new PostDTO()
            {
                Id = p.Id,
                Title = p.Title,
                Category = new CategoryDTO()
                {
                    Name = p.Category.Name
                },
                DateCreated = p.DateCreated,
            });
            var tags = await postRepository.GetTagsAsync(result.Items.Select(p => p.Id), tagExp);
            foreach (var p in result.Items)
                p.Tags = tags.Where(t => t.Id == p.Id).Take(6).Select(t => t.Object).ToList();

            return result;
        }

        public async Task<PostDTO> GetPostViewModel(int id)
        {
            var result = await postRepository.GetAsync(id, (Post p) => new PostDTO()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ShortContent = p.ShortContent,
                Category = new CategoryDTO()
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                },
                DateCreated = p.DateCreated,
                Tags = p.Tags.Select(pt => new TagDTO() { Id = pt.Tag.Id, Name = pt.Tag.Name }).ToList()
            });
            return result;
        }

        public async Task<Post> UpdateOrAddPostIfIdIsNull(int? id, SavePostDTO savePost, IEnumerable<Tag> tags)
        {
            return id.HasValue ? await UpdatePost(id.Value, savePost, tags) : AddPost(savePost, tags);
        }

        public async Task<Post> UpdatePost(int id, SavePostDTO savePost, IEnumerable<Tag> tags)
        {
            savePost.NotNull();

            var post = await postRepository.GetAsync(id, p => p);

            if (post == null)
                return null;

            ValidateShortContent(savePost);

            mapper.Map<SavePostDTO, Post>(savePost, post);
            post.DateLastUpdated = DateTime.Now;
            SetPostTags(post.Tags, tags);

            postRepository.Update(post);

            return post;
        }

        public Post AddPost(SavePostDTO savePost, IEnumerable<Tag> tags)
        {
            savePost.NotNull();

            ValidateShortContent(savePost);

            var post = mapper.Map<SavePostDTO, Post>(savePost);
            post.IsActive = true;
            post.DateCreated = DateTime.Now;
            post.DateLastUpdated = DateTime.Now;
            SetPostTags(post.Tags, tags);

            postRepository.Add(post);

            return post;
        }

        public void Remove(int id)
        {
            postRepository.Delete(new Post() { Id = id });
        }

        private void SetPostTags(ICollection<PostTag> postTags, IEnumerable<Tag> tags)
        {
            if (tags == null || tags.Count() == 0)
            {
                postTags.Clear();
                return;
            }

            if (postTags == null || postTags.Count() == 0)
            {
                foreach (var t in tags)
                    postTags.Add(new PostTag() { TagId = t.Id });
                return;
            }

            // Remove removed tags
            var removedTags = postTags
                .Where(pt => !tags.Any(t => t.Id == pt.TagId))
                .ToList();
            foreach (var t in removedTags)
                postTags.Remove(t);

            // Add new tags
            var addedTags = tags
                .Where(t => !postTags.Any(pt => pt.TagId == t.Id))
                .Select(t => new PostTag() { TagId = t.Id })
                .ToList();
            foreach (var t in addedTags)
                postTags.Add(t);
        }

        private void ValidateShortContent(SavePostDTO savePost)
        {
            if (string.IsNullOrWhiteSpace(savePost.ShortContent))
                savePost.ShortContent = savePost.Content.Substring(0, Math.Min(savePost.Content.Length, 500));
        }
    }
}