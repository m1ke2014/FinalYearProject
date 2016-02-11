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

        public int TimeTaken { get; set; }

        [Display(Name = "Corrective Action")]
        public string CorrectiveAction { get; set; }

        [Display(Name = "Parts Used")]
        public string PartsUsed { get; set; }

       
        public int DeclarationOfConformityID { get; set; }
        public virtual DeclarationOfConformity DOCs { get; set; }

        public int Priorityid { get; set; }
        public virtual Priority Priorities { get; set; }

        public int StaffID { get; set; }
        public virtual Staff StaffMembers { get; set; }

        public int StatusID { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<ServiceCall> ServiceCalls { get; set; }
    }
}