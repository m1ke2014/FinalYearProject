using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class Fault
    {
        [Key]
        public int FaultID { get; set; }
        public string FaultDescription { get; set; }
        public string Solution { get; set; }

        public string ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}