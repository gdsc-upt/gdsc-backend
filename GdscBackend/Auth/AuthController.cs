using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GdscBackend.Auth;

[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userModel;

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
    public async Task<ActionResult<LoginResponse>> Login(LoginViewModel model)
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
            new("name", user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(userRoles.Select(userRole => new Claim("roles", userRole)));

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(5),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return Ok(new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        });
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserViewModel>> Register(RegisterViewModel model)
    {
        var rolesDoesNotExist = !await _roleManager.RoleExistsAsync("admin");
        var admins = await _userModel.GetUsersInRoleAsync("admin");

        if (rolesDoesNotExist || admins.Count == 0)
        {
            await AddRoles();
            return await AddUser(model, true);
        }

        var userWithSameName = await _userModel.FindByNameAsync(model.Username);
        var userWithSameEmail = await _userModel.FindByEmailAsync(model.Email);
        var userExists = userWithSameEmail is not null || userWithSameName is not null;
        if (userExists)
        {
            return BadRequest("User already exists, please login");
        }

        return await AddUser(model, false);
    }

    private async Task AddRoles()
    {
        var role = new Role { Id = Guid.NewGuid().ToString(), Name = "admin" };
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            Console.WriteLine(result.Errors);
        }
    }

    private async Task<ActionResult<UserViewModel>> AddUser(RegisterViewModel model, bool makeAdmin)
    {
        var puser = new User { UserName = model.Username, Email = model.Email };

        var result = await _userModel.CreateAsync(puser, model.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description);
            return new UserViewModel { Errors = errors };
        }

        var user = await _userModel.FindByNameAsync(puser.UserName);
        if (makeAdmin)
        {
            await _userModel.AddToRoleAsync(user, "admin");
        }

        return Created("", new UserViewModel { Id = user.Id, Email = user.Email, UserName = user.UserName });
    }
}
