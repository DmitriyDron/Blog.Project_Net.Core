using Blog.BLL.DTO.Users;
using Blog.BLL.Utilities.Collections;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IPagedList<UserListOutput>> GetUsersAsync(UserListInput input);

        Task<GetUserForCreateOrUpdateOutput> GetUserForCreateOrUpdateAsync(Guid id);

        Task<IdentityResult> AddUserAsync(CreateOrUpdateUserInput input);

        Task<IdentityResult> EditUserAsync(CreateOrUpdateUserInput input);

        Task<IdentityResult> RemoveUserAsync(Guid id);
    }
}