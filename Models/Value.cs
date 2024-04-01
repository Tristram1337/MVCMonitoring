using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class Value
    {
        [Key]
        public int Id { get; set; }

        public int Val { get; set; }

        public DateTime DateTime { get; set; }

        public virtual Station Station { get; set; }
    }
}
