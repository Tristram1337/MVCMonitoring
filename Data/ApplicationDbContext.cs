using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCMonitoring.Models;

namespace MVCMonitoring.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MonitoringStation> Stations { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
    }
}