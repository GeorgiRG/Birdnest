using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Birdnest.Models;

namespace Birdnest.Data
{
    public class BirdnestContext : DbContext
    {


        public DbSet<Birdnest.Models.Pilot> Pilots { get; set; } = default!;
        public DbSet<Birdnest.Models.Sensor> Sensors { get; set; } = default!;
        public DbSet<Birdnest.Models.Violation> Violations { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=mel.db.elephantsql.com;Username=ntbrgpwr;Password=fKbonETossHjUkUaYPhQr8z-BckGRc8y;Database=ntbrgpwr");
    }
}