using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string? Mail { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public string? Gender { get; set; }

        public DateTime DOB { get; set; }

        public string? NIC { get; set; }
    }
}
