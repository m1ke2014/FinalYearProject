using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Product ID")]
        [StringLength(20)]
        public string ProductID { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [StringLength(200)]
        public string ProductDescription { get; set; }

        public string FaultID { get; set; }
        public virtual ICollection<Fault> Faults { get; set; }
    }
}