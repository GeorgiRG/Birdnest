using Microsoft.EntityFrameworkCore;
using Birdnest.Models;
using Birdnest.Secrets;

namespace Birdnest.Data
{
    public class BirdnestContext : DbContext
    {
        private readonly DbSecret conn = new();
        public DbSet<Pilot> Pilots { get; set; } = default!;
        public DbSet<Sensor> Sensors { get; set; } = default!;
        public DbSet<Violation> Violations { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(conn.DBconnection);
    }
}