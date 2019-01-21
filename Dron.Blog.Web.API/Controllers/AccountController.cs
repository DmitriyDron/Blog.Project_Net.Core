using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.DTO.Account;
using Blog.BLL.DTO.Permissions;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.Web.Core.Authentification;
using Blog.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Web.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;
        private readonly IPermissionService _permissionService;

        public AccountController(
            UserManager<User> userManager,
            IOptions<JwtTokenConfiguration> jwtTokenConfiguration,
            IPermissionService permissionService)
        {
            _userManager = userManager;
            _jwtTokenConfiguration = jwtTokenConfiguration.Value;
            _permissionService = permissionService;
        }

     
        [HttpPost("[action]")]
        public async Task<ActionResult<LoginOutput>> Login([FromBody]LoginInput input)
        {
            var userToVerify = await CreateClaimsIdentityAsync(input.UserNameOrEmail, input.Password);
            if (userToVerify == null)
            {

                return BadRequest(new List<NameValueDTO>
                {
                    new NameValueDTO("UserNameOrPasswordIncorrect", "The user name or password is incorrect!")
                });
            }

            var token = new JwtSecurityToken
            (
                issuer: _jwtTokenConfiguration.Issuer,
                audience: _jwtTokenConfiguration.Audience,
                claims: userToVerify.Claims,
                expires: _jwtTokenConfiguration.EndDate,
                notBefore: _jwtTokenConfiguration.StartDate,
                signingCredentials: _jwtTokenConfiguration.SigningCredentials
            );

            return Ok(new LoginOutput { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody]RegisterInput input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user != null)
            {
                return BadRequest(new List<NameValueDTO>
                {
                    new NameValueDTO("EmailAlreadyExist", "This email already exists!")
                });
            }

            var applicationUser = new User
            {
                UserName = input.UserName,
                Email = input.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(applicationUser, input.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => new NameValueDTO(e.Code, e.Description)).ToList());
            }

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordInput input)
        {
            if (input.NewPassword != input.PasswordRepeat)
            {
                return BadRequest(new List<NameValueDTO>
                {
                    new NameValueDTO("PasswordsDoesNotMatch", "Passwords doesn't match!")
                });
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => new NameValueDTO(e.Code, e.Description)).ToList());
            }

            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PermissionDTO>>> GetGrantedPermissionsAsync(string userNameOrEmail)
        {
            return Ok(await _permissionService.GetGrantedPermissionsAsync(userNameOrEmail));
        }

        private async Task<ClaimsIdentity> CreateClaimsIdentityAsync(string userNameOrEmail, string password)
        {
            if (string.IsNullOrEmpty(userNameOrEmail) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var userToVerify = await _userManager.FindByNameAsync(userNameOrEmail) ??
                               await _userManager.FindByEmailAsync(userNameOrEmail);

            if (userToVerify == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userNameOrEmail, "Token"), new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userNameOrEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });
            }

            return null;
        }
    }
}