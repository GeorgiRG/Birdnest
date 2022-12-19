using Birdnest.Models;

namespace Birdnest.ViewModels
{
    public class ViolationsViewModel
    {
        public List<Violation>? AllViolations { get; set; }
        public List<Pilot>? WorstPilots { get; set; }
        public DateTime? LatestData { get; set; }
        public string Title { get; set; } = "Main";

    }
}
