﻿// <auto-generated />
using System;
using Blog.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blog.DAL.Entities.Blog.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastUpdated");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Blog.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastUpdated");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ShortContent")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Blog.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Blog.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastUpdated");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Permissions.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CreateUserId");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<Guid>("UpdateUserId");

                    b.HasKey("Id");

                    b.ToTable("Permission");

                    b.HasData(
                        new { Id = new Guid("2a1ccb43-fa4f-48ce-b601-d3ab4d611b32"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "Administration access", Name = "Permissions_Administration", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("28126ffd-51c2-4201-939c-b64e3df43b9d"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "Member access", Name = "Permissions_Member_Access", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("86d804bd-d022-49a5-821a-d2240478aac4"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "User read", Name = "Permissions_User_Read", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("8f3de3ec-3851-4ba9-887a-2119f18ae744"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "User create", Name = "Permissions_User_Create", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("068a0171-a141-4eb2-854c-88e43ef9ab7f"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "User update", Name = "Permissions_User_Update", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("70b5c5c3-2267-4f7c-b0f9-7ecc952e04a6"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "User delete", Name = "Permissions_User_Delete", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("80562f50-8a7d-4bcd-8971-6d856bbcbb7f"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "Role read", Name = "Permissions_Role_Read", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("d4d7c0e3-efcf-4dd2-86e7-17d69fda8c75"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "Role create", Name = "Permissions_Role_Create", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("ea003a99-4755-4c19-b126-c5cffbc65088"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "Role update", Name = "Permissions_Role_Update", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") },
                        new { Id = new Guid("8f76de0b-114a-4df8-a93d-cec927d06a3c"), CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), CreateUserId = new Guid("00000000-0000-0000-0000-000000000000"), DisplayName = "Role delete", Name = "Permissions_Role_Delete", UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), UpdateUserId = new Guid("00000000-0000-0000-0000-000000000000") }
                    );
                });

            modelBuilder.Entity("Blog.DAL.Entities.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<bool>("IsSystemDefault");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");

                    b.HasData(
                        new { Id = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), ConcurrencyStamp = "7c523e00-bc0a-4228-91f2-66c4040d6b5e", IsSystemDefault = true, Name = "Admin", NormalizedName = "ADMIN" },
                        new { Id = new Guid("11d14a89-3a93-4d39-a94f-82b823f0d4ce"), ConcurrencyStamp = "9422e9c7-62ca-44d4-8d2e-1f34ff10972a", IsSystemDefault = true, Name = "Member", NormalizedName = "MEMBER" }
                    );
                });

            modelBuilder.Entity("Blog.DAL.Entities.Roles.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Roles.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("PermissionId");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("2a1ccb43-fa4f-48ce-b601-d3ab4d611b32") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("28126ffd-51c2-4201-939c-b64e3df43b9d") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("86d804bd-d022-49a5-821a-d2240478aac4") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("8f3de3ec-3851-4ba9-887a-2119f18ae744") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("068a0171-a141-4eb2-854c-88e43ef9ab7f") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("70b5c5c3-2267-4f7c-b0f9-7ecc952e04a6") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("80562f50-8a7d-4bcd-8971-6d856bbcbb7f") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("d4d7c0e3-efcf-4dd2-86e7-17d69fda8c75") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("ea003a99-4755-4c19-b126-c5cffbc65088") },
                        new { RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263"), PermissionId = new Guid("8f76de0b-114a-4df8-a93d-cec927d06a3c") },
                        new { RoleId = new Guid("11d14a89-3a93-4d39-a94f-82b823f0d4ce"), PermissionId = new Guid("28126ffd-51c2-4201-939c-b64e3df43b9d") }
                    );
                });

            modelBuilder.Entity("Blog.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User");

                    b.HasData(
                        new { Id = new Guid("c41a7761-6645-4e2c-b99d-f9e767b9ac77"), AccessFailedCount = 5, ConcurrencyStamp = "c793a217-25a7-4936-a7cc-a8b3bdc4f27d", Email = "admin@mail.com", EmailConfirmed = true, LockoutEnabled = false, NormalizedEmail = "ADMIN@MAIL.COM", NormalizedUserName = "ADMIN", PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==", PhoneNumberConfirmed = false, TwoFactorEnabled = false, UserName = "admin" },
                        new { Id = new Guid("065e903e-6f7b-42b8-b807-0c4197f9d1bc"), AccessFailedCount = 5, ConcurrencyStamp = "95724c0e-4fbf-4094-9a22-acd5acab2955", Email = "memberuser@mail.com", EmailConfirmed = true, LockoutEnabled = false, NormalizedEmail = "MEMBERUSER@MAIL.COM", NormalizedUserName = "MEMBERUSER", PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==", PhoneNumberConfirmed = false, TwoFactorEnabled = false, UserName = "memberuser" },
                        new { Id = new Guid("4b6d9e45-626d-489a-a8cf-d32d36583af4"), AccessFailedCount = 5, ConcurrencyStamp = "2aad78fa-2ca5-4f59-95e9-88694b7b5a02", Email = "testadmin@mail.com", EmailConfirmed = true, LockoutEnabled = false, NormalizedEmail = "TESTADMIN@MAIL.COM", NormalizedUserName = "TESTADMIN", PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==", PhoneNumberConfirmed = false, TwoFactorEnabled = false, UserName = "testadmin" }
                    );
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new { UserId = new Guid("c41a7761-6645-4e2c-b99d-f9e767b9ac77"), RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263") },
                        new { UserId = new Guid("4b6d9e45-626d-489a-a8cf-d32d36583af4"), RoleId = new Guid("f22bce18-06ec-474a-b9af-a9de2a7b8263") },
                        new { UserId = new Guid("065e903e-6f7b-42b8-b807-0c4197f9d1bc"), RoleId = new Guid("11d14a89-3a93-4d39-a94f-82b823f0d4ce") }
                    );
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserToken", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Blog.DAL.Entities.Blog.Post", b =>
                {
                    b.HasOne("Blog.DAL.Entities.Blog.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Blog.PostTag", b =>
                {
                    b.HasOne("Blog.DAL.Entities.Blog.Post", "Post")
                        .WithMany("Tags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blog.DAL.Entities.Blog.Tag", "Tag")
                        .WithMany("Posts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Roles.RoleClaim", b =>
                {
                    b.HasOne("Blog.DAL.Entities.Roles.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Roles.RolePermission", b =>
                {
                    b.HasOne("Blog.DAL.Entities.Permissions.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blog.DAL.Entities.Roles.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserClaim", b =>
                {
                    b.HasOne("Blog.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserLogin", b =>
                {
                    b.HasOne("Blog.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserRole", b =>
                {
                    b.HasOne("Blog.DAL.Entities.Roles.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blog.DAL.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.DAL.Entities.Users.UserToken", b =>
                {
                    b.HasOne("Blog.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
