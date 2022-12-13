using System.ComponentModel.DataAnnotations;

namespace Birdnest.Models
{
    public class Sensor
    {
        public int SensorID { get; set; }
        public float SensorLocationX { get; set; }
        public float SensorLocationY { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}