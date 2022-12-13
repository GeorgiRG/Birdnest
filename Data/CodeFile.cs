using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Birdnest.Models;

namespace Birdnest.Data
{
    public class BirdenstContext : DbContext
    {
        public BirdenstContext(DbContextOptions<BirdenstContext> options)
            : base(options)
        {
        }

        public DbSet<Birdnest.Models.Pilot> Pilots { get; set; } = default!;
        public DbSet<Birdnest.Models.Sensor> Sensors { get; set; } = default!;
        public DbSet<Birdnest.Models.Violation> Violations { get; set; } = default!;

    }
}