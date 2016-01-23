using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class Staff
    {
        public int StaffID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter your email address", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual JobRole JobRole { get; set; }
    }
}