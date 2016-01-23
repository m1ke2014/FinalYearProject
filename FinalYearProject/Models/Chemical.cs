using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class Chemical
    {
        public int ChemicalID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DeclarationOfConformity> DOCs { get; set; }
    }
}