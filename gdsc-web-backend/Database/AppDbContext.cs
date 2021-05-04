using gdsc_web_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ExampleModel> Examples { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<FaqModel> Faqs { get; set; }
        public DbSet<MemberModel> Members { get; set; }
        public DbSet<MenuItemModel> MenuItems { get; set; }
        public DbSet<PageModel> Pages { get; set; }
        public DbSet<SettingModel> Settings { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
    }
}