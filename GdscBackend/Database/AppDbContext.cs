using GdscBackend.Features.Articles;
using GdscBackend.Features.Contacts;
using GdscBackend.Features.Events;
using GdscBackend.Features.Examples;
using GdscBackend.Features.Faqs;
using GdscBackend.Features.FIles;
using GdscBackend.Features.Members;
using GdscBackend.Features.MenuItems;
using GdscBackend.Features.Pages;
using GdscBackend.Features.Redirects;
using GdscBackend.Features.Settings;
using GdscBackend.Features.Teams;
using GdscBackend.Features.Technologies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<RedirectModel>().HasIndex(entity => entity.Path).IsUnique();
        base.OnModelCreating(builder);
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
    public DbSet<TechnologyModel> Technologies { get; set; }
    public DbSet<FileModel> Files { get; set; }
    public DbSet<RedirectModel> Redirects { get; set; }
    public DbSet<ArticleModel> Articles { get; set; }
}