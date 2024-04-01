using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class Measurements
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public virtual MonitoringStation Station { get; set; }
    }
}
