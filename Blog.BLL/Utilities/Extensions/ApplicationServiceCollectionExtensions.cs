using AutoMapper;
using Blog.BLL.Utilities.Extensions.Collections;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Blog.BLL.Utilities.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBlogApplication(this IServiceCollection services)
        {
          
            services.AddAutoMapper(typeof(ApplicationServiceCollectionExtensions));

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}