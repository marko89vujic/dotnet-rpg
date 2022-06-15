using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class FootballWorldDataContext : DbContext
    {
        public FootballWorldDataContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<FootballClub> FootballClubs { get; set; }

        public DbSet<Competition> Competitions { get; set; }
    }
}
