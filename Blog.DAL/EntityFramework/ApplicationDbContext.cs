using Blog.DAL.Entities;
using Blog.DAL.Entities.Blog;
using Blog.DAL.Entities.Permissions;
using Blog.DAL.Entities.Roles;
using Blog.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          

            modelBuilder.Entity<Permission>()
                .ToTable("Permission")
                .HasData(ApplicationDbContextSeeder.BuildPermissions());

            modelBuilder.Entity<Role>().ToTable("Role")
                .HasData(ApplicationDbContextSeeder.BuildApplicationRoles());

            modelBuilder.Entity<User>().ToTable("User")
                .HasData(ApplicationDbContextSeeder.BuildApplicationUsers());

            modelBuilder.Entity((Action<EntityTypeBuilder<RolePermission>>)(b =>
            {
                b.ToTable("RolePermission");
                b.HasKey(rp => new { rp.RoleId, rp.PermissionId });

                b.HasOne(rp => rp.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(pt => pt.RoleId);

                b.HasOne(rp => rp.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(rp => rp.PermissionId);

                b.HasData(ApplicationDbContextSeeder.BuildRolePermissions());
            }));

            modelBuilder.Entity((Action<EntityTypeBuilder<UserRole>>)(b =>
            {
                b.ToTable("UserRole");

                b.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                b.HasData(ApplicationDbContextSeeder.BuildApplicationUserRoles());
            }));
            modelBuilder
                .Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<PostTag>().ToTable("PostTag");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}