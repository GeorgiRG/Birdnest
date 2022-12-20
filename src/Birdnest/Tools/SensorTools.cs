using System.Numerics;
using System.Globalization;

namespace Birdnest.Tools
{
    public class SensorTool
    {
        //values are in millimeters
        public Vector2 Location { get; set; }
        public Vector2 DroneLocation { get; set; }
        public float DetectionRadius { get; set; }
        public int Distance { get; set; }
        public SensorTool(float locationX, float locationY, float detectRadius)
        {
            Distance = -1;
            Location = new Vector2(locationX, locationY);
            DetectionRadius = detectRadius;
        }

        public bool DetectViolation(string dronePositionX, string dronePositionY)
        {
            if (dronePositionX == null || dronePositionY == null)
            {
                return false;
            }
            DroneLocation = new Vector2((float)double.Parse(dronePositionX, CultureInfo.InvariantCulture), (float)double.Parse(dronePositionY, CultureInfo.InvariantCulture));
            float distance = Vector2.Distance(DroneLocation, Location);
            if (distance < DetectionRadius)
            {
                //converting to more readable int values as the precision is already good enough
                Distance = (int)Math.Round(distance);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}