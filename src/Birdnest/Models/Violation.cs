//DroneId to be added for a pilot with multiple drones
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Birdnest.Models
{
    public class Violation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ViolationID { get; set; }
        public float ViolationLocationX { get; set; }
        public float ViolationLocationY { get; set; }
        public int Distance { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
        [Required]
        public string? PilotID { get; set; }
        [Required]
        public Pilot? Pilot { get; set; }
    }
}