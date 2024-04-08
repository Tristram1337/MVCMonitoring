using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class MonitoringStation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
        public int FloodLevel { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
        public int DroughtLevel { get; set; }

        public int TimeOutInMinutes { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}