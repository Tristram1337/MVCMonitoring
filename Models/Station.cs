using System.ComponentModel.DataAnnotations;

namespace MVCMonitoring.Models
{
    public class Station
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required]
        [Range(1, 100)]
        public int FloodLevel { get; set; }

        [Required]
        [Range(1, 100)]

        public int DroughtLevel { get; set; }

        [Required]
        public int TimeOutInMinutes { get; set; }

    }
}
