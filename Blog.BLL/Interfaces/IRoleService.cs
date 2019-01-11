using Blog.BLL.DTO.Roles;
using Blog.BLL.Utilities.Collections;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input);

        Task<GetRoleForCreateOrUpdateOutput> GetRoleForCreateOrUpdateAsync(Guid id);

        Task<IdentityResult> AddRoleAsync(CreateOrUpdateRoleInput input);

        Task<IdentityResult> EditRoleAsync(CreateOrUpdateRoleInput input);

        Task<IdentityResult> RemoveRoleAsync(Guid id);
    }
}