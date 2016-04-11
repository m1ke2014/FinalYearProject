using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class Customer : Person
    {
        [Required]
        public string Company { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}