using gdsc_web_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ExampleModel> Examples;
    }
}