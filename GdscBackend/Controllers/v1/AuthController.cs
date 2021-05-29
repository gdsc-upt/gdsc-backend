using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GdscBackend.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userModel;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userModel, RoleManager<Role> roleManager,
            IConfiguration configuration)
        {
            _userModel = userModel;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userModel.FindByNameAsync(model.Username);
            var wrongPassword = !await _userModel.CheckPasswordAsync(user, model.Password);
            if (user == null || wrongPassword)
            {
                return Unauthorized();
            }

            var userRoles = await _userModel.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var rolesDoesNotExist = !await _roleManager.RoleExistsAsync("admin");

            if (rolesDoesNotExist)
            {
                await AddRoles();
                await AddUser(model, true);
            }
            else
            {
                var userExists = await _userModel.FindByNameAsync(model.Username);
                if (userExists != null)
                    return BadRequest("User already exists");

                await AddUser(model, false);
            }

            return Ok();
        }

        private async Task AddRoles()
        {
            var role = new Role {Id = Guid.NewGuid().ToString(), Name = "admin"};
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) Console.WriteLine(result.Errors);
        }

        private async Task AddUser(RegisterViewModel model, bool makeAdmin)
        {
            var puser = new User {UserName = model.Username, Email = model.Email};

            await _userModel.CreateAsync(puser, model.Password);
            var user = await _userModel.FindByNameAsync(puser.UserName);
            if (makeAdmin) await _userModel.AddToRoleAsync(user, "admin");
        }
    }
}
