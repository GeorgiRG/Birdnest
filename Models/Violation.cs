//DroneId to be added for a pilot with multiple drones
using System;
using System.ComponentModel.DataAnnotations;

namespace Birdnest.Models
{
    public class Violation
    {
        public int ViolationID { get; set; }
        public float ViolationLocationX { get; set; }
        public float ViolationLocationY { get; set; }
        public int Distance { get; set; }
        public DateTime Time { get; set; }
        public int PilotID { get; set; }
        [Required]
        public Pilot? Pilot { get; set; }
    }
}