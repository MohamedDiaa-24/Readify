using System.ComponentModel.DataAnnotations;

namespace Readify.Models
{
    public class Company
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }




    }
}
