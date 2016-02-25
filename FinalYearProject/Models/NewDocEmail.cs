using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class NewDocEmail : Email
    {
        public string To { get; set; }
        public string Customer { get; set; }
        public string Comment { get; set; }
    }
}