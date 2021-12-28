namespace CustomerRegistration.Models.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }

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
