using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Birdnest.Models
{
    public class Pilot
    {
        public int PilotID { get; set; }
        public string? Name { get; set; }
        [Required]
        public List<Violation>? Violations { get; set; }

    }
}