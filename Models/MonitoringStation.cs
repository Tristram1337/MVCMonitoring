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

        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}