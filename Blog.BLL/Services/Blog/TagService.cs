using Blog.BLL.DTO.Blog;
using Blog.BLL.Interfaces.Blog;
using Blog.DAL.Entities.Blog;
using Blog.DAL.Extensions;
using Blog.DAL.Interfaces.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services.Blog
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<IEnumerable<Tag>> FindByNamesAndAddIfNotExists(IEnumerable<string> tags)
        {
            tags.NotNull();

            var existingTags = await tagRepository.FindByNamesAsync(tags, t => t);
            var missingNames = tags.Where(t => !existingTags.Any(e => e.Name == t));

            if (missingNames == null || missingNames.Count() == 0)
                return existingTags;

            var dateCreated = DateTime.Now;
            var missingTags = missingNames.Select(name => new Tag()
            {
                Name = name,
                IsActive = true,
                DateCreated = dateCreated,
                DateLastUpdated = dateCreated
            }).ToList();
            tagRepository.AddRange(missingTags);

            missingTags.AddRange(existingTags);
            return missingTags;
        }

        public async Task<IEnumerable<TagDTO>> FindFirsTagsLike(string name, int numOfRecords)
        {
            if (string.IsNullOrWhiteSpace(name) || numOfRecords <= 0)
                return new List<TagDTO>();

            return await tagRepository.FindByNameAsync(name, numOfRecords, t => new TagDTO()
            {
                Id = t.Id,
                Name = t.Name
            });
        }
    }
}