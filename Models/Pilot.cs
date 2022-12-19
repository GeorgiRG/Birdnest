using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Birdnest.Models
{
    public class Pilot
    {
        [Required]
        public string? PilotID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [AllowNull]
        public Violation Violations { get; set; }

    }
}