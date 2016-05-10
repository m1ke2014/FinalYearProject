using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class JobRole
    {
        public int JobRoleID { get; set; }
        public string JobTitle { get; set; }

        public virtual Privlege Privleges { get; set; }
    }
}