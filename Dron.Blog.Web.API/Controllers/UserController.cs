using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.DTO.Users;
using Blog.BLL.Interfaces;
using Blog.BLL.Utilities.Collections;
using Blog.DAL.Entities.Permissions;
using Blog.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.API.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserRead)]
        public async Task<ActionResult<IPagedList<UserListOutput>>> GetUsers(UserListInput input)
        {
            return Ok(await _userService.GetUsersAsync(input));
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserCreate)]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserUpdate)]
        public async Task<ActionResult<GetUserForCreateOrUpdateOutput>> GetUserForCreateOrUpdate(Guid id)
        {
            var getUserForCreateOrUpdateOutput = await _userService.GetUserForCreateOrUpdateAsync(id);

            return Ok(getUserForCreateOrUpdateOutput);
        }

        [HttpPost("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserCreate)]
        public async Task<ActionResult> CreateOrUpdateUser([FromBody]CreateOrUpdateUserInput input)
        {
            IdentityResult identityResult;
            if (input.User.Id == Guid.Empty)
            {
                identityResult = await _userService.AddUserAsync(input);
            }
            else
            {
                identityResult = await _userService.EditUserAsync(input);
            }

            if (identityResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(identityResult.Errors.Select(e => new NameValueDTO(e.Code, e.Description)));
        }

        [HttpDelete("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserDelete)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var identityResult = await _userService.RemoveUserAsync(id);

            if (identityResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(identityResult.Errors.Select(e => new NameValueDTO(e.Code, e.Description)));
        }
    }
}