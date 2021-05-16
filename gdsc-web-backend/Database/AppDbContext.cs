using gdsc_web_backend.Authentication;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<ExampleModel> Examples;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}