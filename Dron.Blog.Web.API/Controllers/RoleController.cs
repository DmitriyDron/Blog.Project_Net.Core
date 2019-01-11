using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.DTO.Roles;
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
    public class RoleController : AdminController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleRead)]
        public async Task<ActionResult<IPagedList<RoleListOutput>>> GetRoles(RoleListInput input)
        {
            return Ok(await _roleService.GetRolesAsync(input));
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleCreate)]
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleUpdate)]
        public async Task<ActionResult<GetRoleForCreateOrUpdateOutput>> GetRoleForCreateOrUpdate(Guid id)
        {
            var getRoleForCreateOrUpdateOutput = await _roleService.GetRoleForCreateOrUpdateAsync(id);

            return Ok(getRoleForCreateOrUpdateOutput);
        }

        [HttpPost("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleCreate)]
        public async Task<ActionResult> CreateOrUpdateRole([FromBody]CreateOrUpdateRoleInput input)
        {
            IdentityResult identityResult;
            if (input.Role.Id == Guid.Empty)
            {
                identityResult = await _roleService.AddRoleAsync(input);
            }
            else
            {
                identityResult = await _roleService.EditRoleAsync(input);
            }

            if (identityResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(identityResult.Errors.Select(e => new NameValueDTO(e.Code, e.Description)));
        }

        [HttpDelete("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleDelete)]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            var identityResult = await _roleService.RemoveRoleAsync(id);

            if (identityResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(identityResult.Errors.Select(e => new NameValueDTO(e.Code, e.Description)));
        }
    }
}