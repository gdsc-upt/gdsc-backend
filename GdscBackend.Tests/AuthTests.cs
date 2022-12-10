using System.Text;
using GdscBackend.Auth;
using GdscBackend.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests;

public class AuthTests : TestingBase
{
    private readonly IConfiguration _configuration;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public AuthTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        var builder = WebApplication.CreateBuilder();
        var services = builder.Services;
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:5000",
                    ValidIssuer = "http://localhost:5000",
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somerandomsecretherefortesting"))
                };
            });

        // Taken from https://github.com/aspnet/MusicStore/blob/dev/test/MusicStore.Test/ManageControllerTest.cs (and modified)
        // IHttpContextAccessor is required for SignInManager, and UserManager
        var context = new DefaultHttpContext();
        context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
        services.AddSingleton<IHttpContextAccessor>(_ => new HttpContextAccessor { HttpContext = context });
        var serviceProvider = services.BuildServiceProvider();
        _userManager = serviceProvider.GetService<UserManager<User>>();
        _roleManager = serviceProvider.GetService<RoleManager<Role>>();
        _configuration = serviceProvider.GetService<IConfiguration>();
    }

    [Fact]
    public async void Register()
    {
        // Arrange
        var user = new RegisterViewModel
        {
            Username = "UserForTesting",
            Email = "testuser@dscupt.tech",
            Password = "sometestpasswordhere"
        };

        var controller = new AuthController(_userManager, _roleManager, _configuration);

        // Act
        var added = await controller.Register(user);

        // Assert
        var actionResult = Assert.IsType<ActionResult<UserViewModel>>(added);
        Assert.NotNull(actionResult);
        var createdResult = Assert.IsType<CreatedResult>(actionResult.Result);
        Assert.NotNull(createdResult);
        var entity = Assert.IsType<UserViewModel>(createdResult.Value);

        Assert.NotNull(entity);
        Assert.NotNull(entity.UserName);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        Assert.Equal(user.Username, entity.UserName);
        Assert.Equal(user.Email, entity.Email);
    }

    [Fact]
    public async void Login()
    {
        // Arrange
        var controller = new AuthController(_userManager, _roleManager, _configuration);

        var userToRegister = new User { UserName = "basicUsername", Email = "basicEmail" };

        await _userManager.CreateAsync(userToRegister, "basicPassword");

        var userToLogin = new LoginViewModel
        {
            Username = "basicUsername",
            Password = "basicPassword"
        };

        // Act
        var added = await controller.Login(userToLogin);

        // Assert
        Assert.NotNull(added);
        var actionResult = Assert.IsType<ActionResult<LoginResponse>>(added);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var result = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.NotNull(result.Token);
    }
}
