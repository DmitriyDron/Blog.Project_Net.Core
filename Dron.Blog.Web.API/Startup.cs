using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.BLL.Interfaces;
using Blog.BLL.Interfaces.Blog;
using Blog.BLL.Services;
using Blog.BLL.Services.Blog;
using Blog.BLL.Utilities.Extensions;
using Blog.DAL.Interfaces.Blog;
using Blog.DAL.Interfaces.UoW;
using Blog.DAL.Repositories.Blog;
using Blog.DAL.UoW;
using Blog.Web.Core.ActionFilters;
using Blog.Web.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Swagger;

namespace Dron.Blog.Web.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            RepositoryImplementation(services);
            ServiceImplementation(services);
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.ConfigureDbContext(_configuration);
            services.ConfigureAuthentication(_configuration);
            services.ConfigureJwtTokenAuth(_configuration);
            services.ConfigureCors(_configuration);
            services.ConfigureDependencyInjection();
            services.ConfigureBlogApplication();

            services.AddMvc(setup =>
            {
                setup.Filters.AddService<UoWActionFilter>();
            });



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Blog API", Version = "v1" });
            });
        }
        private void RepositoryImplementation(IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
        }
        private void ServiceImplementation(IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
             if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
                HotModuleReplacement = true,
              //  HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
              app.UseHsts();
        }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
            });

            app.UseCors(_configuration["App:CorsOriginPolicyName"]);
            app.UseAuthentication();

            app.UseMvc();

            var provider = new FileExtensionContentTypeProvider();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString(""),
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

