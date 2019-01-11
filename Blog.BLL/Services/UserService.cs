using AutoMapper;
using Blog.BLL.DTO.Roles;
using Blog.BLL.DTO.Users;
using Blog.BLL.Interfaces;
using Blog.BLL.Utilities.Collections;
using Blog.BLL.Utilities.Extensions.Collections;
using Blog.BLL.Utilities.Extensions.PrimitiveTypes;
using Blog.DAL.Entities;
using Blog.DAL.Entities.Users;
using Blog.DAL.EntityFramework;
using Blog.DAL.Interfaces.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public UserService(IMapper mapper,IUnitOfWork unitOfWork,
        UserManager<User> userManager,
            ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IPagedList<UserListOutput>> GetUsersAsync(UserListInput input)
        {
            var query = _unitOfWork.Repository<User>().Query().Where(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.UserName.Contains(input.Filter) ||
                                 predicate.Email.Contains(input.Filter))
                .OrderBy(input.SortBy);

            var usersCount = await query.CountAsync();
            var users = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var userListOutput = _mapper.Map<List<UserListOutput>>(users);

            return userListOutput.ToPagedList(usersCount);
        }

        public async Task<GetUserForCreateOrUpdateOutput> GetUserForCreateOrUpdateAsync(Guid id)
        {
            var allRoles = _mapper.Map<List<RoleDTO>>(_dbContext.Roles).OrderBy(r => r.Name).ToList();
            var getUserForCreateOrUpdateOutput = new GetUserForCreateOrUpdateOutput
            {
                AllRoles = allRoles
            };

            if (id == Guid.Empty)
            {
                return getUserForCreateOrUpdateOutput;
            }

            return await GetUserForCreateOrUpdateOutputAsync(id, allRoles);
        }

        public async Task<IdentityResult> AddUserAsync(CreateOrUpdateUserInput input)
        {
            var user = new User
            {
                Id = input.User.Id,
                UserName = input.User.UserName,
                Email = input.User.Email
            };

            var createUserResult = await _userManager.CreateAsync(user, input.User.Password);
            if (createUserResult.Succeeded)
            {
                GrantRolesToUser(input.GrantedRoleIds, user);
            }

            return createUserResult;
        }

        public async Task<IdentityResult> EditUserAsync(CreateOrUpdateUserInput input)
        {
            var user = await _userManager.FindByIdAsync(input.User.Id.ToString());
            if (user.UserName == input.User.UserName && user.Id != input.User.Id)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNameAlreadyExist",
                    Description = "This user name is already exist!"
                });
            }

            if (!input.User.Password.IsNullOrEmpty())
            {
                var changePasswordResult = await ChangePassword(user, input.User.Password);
                if (!changePasswordResult.Succeeded)
                {
                    return changePasswordResult;
                }
            }

            return await UpdateUser(input, user);
        }

        public async Task<IdentityResult> RemoveUserAsync(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found!"
                });
            }

            if (user.UserName.Contains(DefaultUsers.Admin.UserName))
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "CannotRemoveAdminUser",
                    Description = "You cannot remove admin user!"
                });
            }

            var removeUserResult = await _userManager.DeleteAsync(user);
            if (!removeUserResult.Succeeded)
            {
                return removeUserResult;
            }

            user.UserRoles.Clear();

            return removeUserResult;
        }

        private void GrantRolesToUser(IEnumerable<Guid> grantedRoleIds, User user)
        {
            foreach (var roleId in grantedRoleIds)
            {
                _dbContext.UserRoles.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = user.Id
                });
            }
        }

        private async Task<IdentityResult> ChangePassword(User user, string password)
        {
            var changePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (changePasswordResult.Succeeded)
            {
                changePasswordResult = await _userManager.AddPasswordAsync(user, password);
            }

            return changePasswordResult;
        }

        private async Task<IdentityResult> UpdateUser(CreateOrUpdateUserInput input, User user)
        {
            user.UserName = input.User.UserName;
            user.Email = input.User.Email;
            user.UserRoles.Clear();
            user.SecurityStamp = Guid.NewGuid().ToString();

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (updateUserResult.Succeeded)
            {
                GrantRolesToUser(input.GrantedRoleIds, user);
            }

            return updateUserResult;
        }

        private async Task<GetUserForCreateOrUpdateOutput> GetUserForCreateOrUpdateOutputAsync(Guid id, List<RoleDTO> allRoles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userDto = _mapper.Map<UserDTO>(user);
            var grantedRoles = user.UserRoles.Select(ur => ur.Role);

            return new GetUserForCreateOrUpdateOutput
            {
                User = userDto,
                AllRoles = allRoles,
                GrantedRoleIds = grantedRoles.Select(r => r.Id).ToList()
            };
        }
    }
}