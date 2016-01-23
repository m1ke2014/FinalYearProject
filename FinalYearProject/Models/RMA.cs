using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class RMA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "RMA Number")]
        public int RMAid { get; set; }

        public string Priority { get; set; }
        public int TimeTaken { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<ServiceCall> ServiceCalls { get; set; }
    }
}