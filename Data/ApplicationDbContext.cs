using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCMonitoring.Models;

namespace MVCMonitoring.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<MonitoringStation> Stations { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
    }
}