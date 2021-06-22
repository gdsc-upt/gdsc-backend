using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Models
{
    public class MemberTeamModel : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Server=pga.dscupt.tech;Database=myDataBase;
                                                Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<MemberModel> Members { get; set; }
    }
}