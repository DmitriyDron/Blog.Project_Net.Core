using AutoMapper;
using Blog.BLL.DTO.Permissions;
using Blog.DAL.Entities;
using Blog.DAL.EntityFramework;
using Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities.Permissions;
using Blog.DAL.Entities.Roles;

namespace Blog.BLL.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
       
        public PermissionService(
            UserManager<User> userManager,
            IMapper mapper,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PermissionDTO>> GetGrantedPermissionsAsync(string userNameOrEmail)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u =>
                u.UserName == userNameOrEmail || u.Email == userNameOrEmail);

            var grantedPermissions = user?.UserRoles
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission);

            return _mapper.Map<IEnumerable<PermissionDTO>>(grantedPermissions);
        }

        public async Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u =>
                u.UserName == userNameOrEmail || u.Email == userNameOrEmail);
            if (user == null)
            {
                return false;
            }

            var grantedPermissions = user.UserRoles
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission);

            return grantedPermissions.Any(p => p.Name == permissionName);
        }

        public void InitializePermissions(List<Permission> permissions)
        {
            _dbContext.RolePermissions.RemoveRange(_dbContext.RolePermissions.Where(rp => rp.RoleId == DefaultRoles.Admin.Id));
            _dbContext.SaveChanges();

            _dbContext.Permissions.RemoveRange(_dbContext.Permissions);
            _dbContext.SaveChanges();

            _dbContext.AddRange(permissions);
            GrantAllPermissionsToAdminRole(permissions);
            _dbContext.SaveChanges();
        }

        private void GrantAllPermissionsToAdminRole(List<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                _dbContext.RolePermissions.Add(new RolePermission
                {
                    PermissionId = permission.Id,
                    RoleId = DefaultRoles.Admin.Id
                });
            }
        }
    }
}