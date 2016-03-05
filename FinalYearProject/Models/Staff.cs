using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public string FullName
        {
            get
            {
                return FirstName + " " + Surname;
            }
        }

        public int RMAid { get; set; }
        public virtual ICollection<RMA> RMAs { get; set; }

        //public virtual JobRole JobRole { get; set; }


    }

}