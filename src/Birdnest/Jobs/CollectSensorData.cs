using Quartz;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Birdnest.Tools;
using Birdnest.Models;
using Birdnest.Data;

namespace Birdnest.Jobs
{
    public class CollectSensorData : IJob
    {
        private static readonly HttpClient client = new()
        {
            BaseAddress = new Uri("http://assignments.reaktor.com/birdnest/")
        };
        private readonly BirdnestContext db = new();


        public async Task Execute(IJobExecutionContext context)
        {
            List<Sensor> sensors = await db.Sensors.ToListAsync();
            string sensorData = await client.GetStringAsync("drones");
            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(sensorData);
            XmlNodeList droneList = xmlDoc.GetElementsByTagName("drone");

            for (int i = 0; i < sensors.Count; i++)
            {
                Console.WriteLine("\nNew");
                //add detection distance instead of hardcode
                SensorTool sensorTool = new(sensors[i].SensorLocationX, sensors[i].SensorLocationY, sensors[i].DetectionDistance);

                foreach (XmlElement drone in droneList)
                {
                    if (sensorTool.DetectViolation(drone["positionX"]!.InnerText.ToString(), drone["positionY"]!.InnerText.ToString()))
                    {
                        //pilot data
                        string serialNumber = drone["serialNumber"]!.InnerText.ToString();
                        string pilotConn = $"pilots/{serialNumber}";
                        Console.WriteLine("Detected violation " + pilotConn);
                        Pilot? badPilot = await client.GetFromJsonAsync<Pilot>(pilotConn);
                        if (badPilot == null)
                        {
                            Console.WriteLine("No pilot found");
                            break;
                        }
                        Pilot? oldPilot = await db.Pilots.FirstOrDefaultAsync(oldPilot => oldPilot.PilotID == badPilot!.PilotID);

                        //if the pilot is in the db, update the violation time, duration and closest distance if closer
                        if (oldPilot != null)
                        {
                            Violation? violation = await db.Violations.FirstOrDefaultAsync(violation => violation.PilotID == oldPilot.PilotID);
                            if (violation == null)
                            {
                                Console.WriteLine("No violation found");
                                break;
                            }
                            if (sensorTool.Distance < violation.Distance)
                            {
                                violation.ViolationLocationX = sensorTool.DroneLocation.X;
                                violation.ViolationLocationY = sensorTool.DroneLocation.Y;
                                violation.Distance = sensorTool.Distance;
                            }
                            violation.Time = DateTime.UtcNow;
                            violation.Duration += 2; //as data updates every 2sec
                            db.SaveChanges();

                        }
                        //create new violation and pilot
                        else
                        {
                            Violation violation = new()
                            {
                                ViolationLocationX = sensorTool.DroneLocation.X,
                                ViolationLocationY = sensorTool.DroneLocation.Y,
                                Distance = sensorTool.Distance,
                                Time = DateTime.UtcNow,
                                Duration = 2,
                                PilotID = badPilot.PilotID,
                                Pilot = badPilot
                            };
                            badPilot.Violations = violation;
                            db.Pilots.Add(badPilot);
                            db.Violations.Add(violation);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No violation" + ", " + drone["positionX"]!.InnerText.ToString() + ", " + drone["positionY"]!.InnerText.ToString());
                    }
                }
            }
            //clean all data older than 10min
            List<Violation> olderViolations = await db.Violations.Where(old => old.Time.AddMinutes(10) < DateTime.UtcNow).ToListAsync();
            foreach (Violation olderViolation in olderViolations)
            {
                Pilot olderPilot = await db.Pilots.FirstAsync(olderPilot => olderPilot.Violations.Equals(olderViolation));
                db.Pilots.Remove(olderPilot);
                db.Violations.Remove(olderViolation);
                db.SaveChanges();
            }
            db.Dispose();

        }
    }
}
