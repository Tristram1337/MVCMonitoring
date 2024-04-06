using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 100)]
        public int FloodLevel { get; set; }

        [Required]
        [Range(1, 100)]
        public int DroughtLevel { get; set; }

        [Required]
        public int TimeOutInMinutes { get; set; }

        public DateTime DateTime { get; set; }

        public int StationId { get; set; }

        public virtual MonitoringStation Station { get; set; }
    }
}