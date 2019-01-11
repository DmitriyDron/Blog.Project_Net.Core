using Blog.DAL.Entities;
using Blog.DAL.Entities.Permissions;
using Blog.DAL.Entities.Roles;
using Blog.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.DAL.EntityFramework
{
    class ApplicationDbContextSeeder
    {
        #region BuildData
        public static User[] BuildApplicationUsers()
        {
            return DefaultUsers.All().ToArray();
        }

        public static Role[] BuildApplicationRoles()
        {
            return DefaultRoles.All().ToArray();
        }

        public static UserRole[] BuildApplicationUserRoles()
        {
            return new[]
            {
                //admin role to admin user
                new UserRole
                {
                    RoleId = DefaultRoles.Admin.Id,
                    UserId = DefaultUsers.Admin.Id
                },
                new UserRole
                {
                    RoleId = DefaultRoles.Admin.Id,
                    UserId = DefaultUsers.TestAdmin.Id
                },
                //member role to member user
                new UserRole
                {
                    RoleId = DefaultRoles.Member.Id,
                    UserId = DefaultUsers.Member.Id
                }
            };
        }

        public static Permission[] BuildPermissions()
        {
            return DefaultPermissions.All().ToArray();
        }

        public static RolePermission[] BuildRolePermissions()
        {
            //grant all permissions to admin role
            var rolePermissions = DefaultPermissions.All().Select(p =>
                new RolePermission
                {
                    PermissionId = p.Id,
                    RoleId = DefaultRoles.Admin.Id
                }).ToList();

            //grant member access permission to member role
            rolePermissions.Add(new RolePermission
            {
                PermissionId = DefaultPermissions.MemberAccess.Id,
                RoleId = DefaultRoles.Member.Id
            });

            return rolePermissions.ToArray();
        }
        #endregion
    }
}