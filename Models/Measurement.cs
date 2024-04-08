using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 100)]
        public int WaterLevel { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public int StationId { get; set; }

        public virtual MonitoringStation Station { get; set; }
    }
}