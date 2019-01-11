using AutoMapper;
using Blog.BLL.DTO.Blog;
using Blog.BLL.DTO.Blog.Query;
using Blog.BLL.DTO.Blog.Save;
using Blog.BLL.DTO.Users;
using Blog.DAL.Entities;
using Blog.DAL.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.BLL.Utilities.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMapPost();
            CreateMapCategory();
            CreateMap<PostQueryDTO, PostQuery>();
            CreateMap<Tag, TagDTO>();
        }
        private void CreateMapPost()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(pvm => pvm.Tags, opt => opt.MapFrom(p => p.Tags.Select(t => new TagDTO() { Id = t.Tag.Id, Name = t.Tag.Name })));

            CreateMap<SavePostDTO, Post>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.IsActive, opt => opt.Ignore())
                .ForMember(v => v.DateCreated, opt => opt.Ignore())
                .ForMember(v => v.DateLastUpdated, opt => opt.Ignore())
                .ForMember(v => v.Category, opt => opt.Ignore())
                .ForMember(v => v.Tags, opt => opt.Ignore())
                ;
        }

        private void CreateMapCategory()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<SaveCategoryDTO, Category>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.IsActive, opt => opt.Ignore())
                .ForMember(v => v.DateCreated, opt => opt.Ignore())
                .ForMember(v => v.DateLastUpdated, opt => opt.Ignore())
                .ForMember(v => v.Posts, opt => opt.Ignore())
                ;
        }
    }
}