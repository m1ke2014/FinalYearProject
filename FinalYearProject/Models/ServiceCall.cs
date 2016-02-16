using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class ServiceCall
    {
        public int ServiceCallID { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }

        public int RMAid { get; set; }
        public virtual RMA RMA { get; set; }

        public string ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}