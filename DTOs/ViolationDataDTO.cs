//Merges Pilot and Violation data

namespace Birdnest.DTOs
{
    public class ViolationDataDTO
    {
        public string? PilotID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public float ViolationLocationX { get; set; }
        public float ViolationLocationY { get; set; }
        public int Distance { get; set; }
        public string? Time { get; set; }
        public int Duration { get; set; }
    }
}
