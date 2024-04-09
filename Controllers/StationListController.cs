using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMonitoring.Data;

namespace MVCMonitoring.Controllers
{
    [Route("List")]
    public class StationListController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult List()
        {
            var stationsWithMeasurements = _context.Stations
                .Include(s => s.Measurements)
                .ToList();

            return View(stationsWithMeasurements);
        }

        // Just messing around

        //public IActionResult List()
        //{
        //    var stationsWithMeasurements = _context.Stations
        //        .Include(s => s.Measurements)
        //        .ToList();

        //    foreach (var station in stationsWithMeasurements)
        //    {
        //        if (station.Measurements != null && station.Measurements.Count != 0)
        //        {
        //            var lastMeasurement = station.Measurements.Last();
        //            if ((DateTime.Now - lastMeasurement.DateTime).TotalMinutes > station.TimeOutInMinutes)
        //            {
        //                SendTimeoutNotification("recipient@example.com");
        //            }
        //            if (lastMeasurement.WaterLevel < station.DroughtLevel || lastMeasurement.WaterLevel > station.FloodLevel)
        //            {
        //                SendWaterLevelWarning("recipient@example.com", lastMeasurement.WaterLevel, station.DroughtLevel, station.FloodLevel);
        //            }
        //        }
        //    }
        //    return View(stationsWithMeasurements);
        //}

        //private static void SendWaterLevelWarning(string recipientEmail, int waterLevel, int droughtLevel, int floodLevel)
        //{
        //    var emailMessage = new MailMessage
        //    {
        //        From = new MailAddress("no-reply@monitoringsystem.com")
        //    };
        //    emailMessage.To.Add(recipientEmail);
        //    emailMessage.Subject = "Water Level Warning";
        //    emailMessage.Body = $"The current water level of {waterLevel} is outside the safe range of {droughtLevel} to {floodLevel}.";

        //    using var client = new SmtpClient("smtp.mailtrap.io", 465);
        //    client.Credentials = new NetworkCredential("username", "password");
        //    client.EnableSsl = true;
        //    client.Send(emailMessage);
        //}

        //private static void SendTimeoutNotification(string recipientEmail)
        //{
        //    var emailMessage = new MailMessage
        //    {
        //        From = new MailAddress("no-reply@monitoringsystem.com")
        //    };
        //    emailMessage.To.Add(recipientEmail);
        //    emailMessage.Subject = "Timeout Exceeded";
        //    emailMessage.Body = "The timeout has been exceeded.";

        //    using var client = new SmtpClient("smtp.mailtrap.io", 465);
        //    client.Credentials = new NetworkCredential("username", "password");
        //    client.EnableSsl = true;
        //    client.Send(emailMessage);
        //}
    }
}