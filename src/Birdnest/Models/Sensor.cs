using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Birdnest.Models
{
    public class Sensor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensorID { get; set; }
        public float SensorLocationX { get; set; }
        public float SensorLocationY { get; set; }
        public float DetectionDistance { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}