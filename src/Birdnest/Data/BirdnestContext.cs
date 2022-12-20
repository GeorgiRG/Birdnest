using Microsoft.EntityFrameworkCore;
using Birdnest.Models;

namespace Birdnest.Data
{
    public class BirdnestContext : DbContext
    {


        public DbSet<Pilot> Pilots { get; set; } = default!;
        public DbSet<Sensor> Sensors { get; set; } = default!;
        public DbSet<Violation> Violations { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=mel.db.elephantsql.com;Username=ntbrgpwr;Password=fKbonETossHjUkUaYPhQr8z-BckGRc8y;Database=ntbrgpwr");
    }
}