using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GdscBackend.Authentication;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GdscBackend.Controllers.v1
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userModel;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userModel, RoleManager<Role> roleManager,
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
            if (user == null || !await _userModel.CheckPasswordAsync(user, model.Password))
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
            // check if there is at least one user, if not create one

            var roleExists = await _roleManager.RoleExistsAsync("admin");


            
            if (!roleExists)
            {
                addRoles();
                await addUser(model, true);
            }
            else
            {
                var userExists = await _userModel.FindByNameAsync(model.Username);
                if (userExists != null)
                    return BadRequest("User already exists");
                
                await addUser(model, false);
            }
            

            /*var user = new User();
            user.Email = model.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = model.Username;
            user.Id = Guid.NewGuid().ToString();
            
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };*/
            

            /*var result = await _userModel.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest("User creation failed! Please check user details and try again.");*/
            
            //user = await _userModel.FindByNameAsync(model.Username);
            //return CreatedAtAction(nameof(Register), new {user.Id}, model);
            
            return Ok();
        }

        private void addRoles()
        {
            //var role = new IdentityRole();
            var role = new Role();
            role.Name = "admin";
            _roleManager.CreateAsync(role);
        }
        
        private async Task<ActionResult<User>> addUser(RegisterViewModel registerViewModel, bool isAdmin)
        {
            var puser = new User {UserName = registerViewModel.Username, Email = registerViewModel.Email};

            var powerUser = await _userModel.CreateAsync(puser, registerViewModel.Password);
            if (!powerUser.Succeeded) return BadRequest(powerUser.Errors);
            var user = await _userModel.FindByNameAsync(puser.UserName);
            if (isAdmin)
                await _userModel.AddToRoleAsync(user, "admin");
            return user;

        }
    }

}
