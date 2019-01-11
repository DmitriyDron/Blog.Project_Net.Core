using Blog.DAL.Interfaces.Repositories;
using Blog.DAL.Interfaces.UoW;
using Blog.DAL.Repositories;
using Blog.DAL.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.EntityFramework
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogEntityFramework(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationDbContext>() ;

            return services;
        }
    }
}
