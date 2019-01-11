using Blog.BLL.DTO.Permissions;
using Blog.DAL.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDTO>> GetGrantedPermissionsAsync(string userNameOrEmail);

        Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName);

        void InitializePermissions(List<Permission> permissions);
    }
}