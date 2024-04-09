using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
        public int WaterLevel { get; set; }

        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Station Id is required")]
        public int StationId { get; set; }

        public virtual MonitoringStation Station { get; set; }
    }
}