using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class WorkAllocation
    {
        public int ID { get; set; }
        public string CurrentWorkLoad { get; set; }
        public string WorkToBeAllocated { get; set; }

        public virtual RMA RMA { get; set; }
        public virtual Staff Staff { get; set; }
    }
}