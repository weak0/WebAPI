using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CreateRestuarantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
