using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        [Required]
        public string Status { get; set; } // "Tersedia" or "Disewa"
    }
}
