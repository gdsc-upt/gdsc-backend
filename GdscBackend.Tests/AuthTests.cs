using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FactoryBot;
using Faker;
using GdscBackend.Auth;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.Models.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class AuthTests : TestingBase
    {
        private AppDbContext Context { get; }
        private UserManager<User> _userManager { get; }
        
        private RoleManager<Role> _roleManager;
        private IConfiguration _configuration;
        
        public AuthTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            var services = new ServiceCollection();
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<AppDbContext>();
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somerandomsecretherefortesting"))
                    };
                });
            
            // Taken from https://github.com/aspnet/MusicStore/blob/dev/test/MusicStore.Test/ManageControllerTest.cs (and modified)
            // IHttpContextAccessor is required for SignInManager, and UserManager
            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(_ => new HttpContextAccessor { HttpContext = context });
            var serviceProvider = services.BuildServiceProvider();
            Context = serviceProvider.GetRequiredService<AppDbContext>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
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

            var result = added.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            var entity = result.Value as RegisterViewModel;

            Assert.NotNull(entity);
            Assert.NotNull(entity.Username);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(user, entity);
        }

        [Fact]
        public async void Login()
        {
            // Arrange
            var controller = new AuthController(_userManager, _roleManager, _configuration);

            var userToRegister = new User {UserName = "basicUsername", Email = "basicEmail"};
            
            await _userManager.CreateAsync(userToRegister, "basicPassword");

            var userToLogin = new LoginViewModel()
            {
                Username = "basicUsername",
                Password = "basicPassword"
            };

            // Act
            var added = await controller.Login(userToLogin);
            var result = added as ViewResult;
            
            // Assert
            Assert.NotNull(result);
        }
        
    }
}