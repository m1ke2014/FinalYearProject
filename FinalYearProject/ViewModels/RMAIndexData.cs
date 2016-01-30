using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.ViewModels
{
    public class RMAIndexData
    {
        public IEnumerable<RMA> RMAs { get; set; }
        public IEnumerable<DeclarationOfConformity> DOCs { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<Status> Status { get; set; }
        public IEnumerable<ServiceCall> ServiceCalls { get; set; }
        public IEnumerable<Priority> Priorities { get; set; }
    }
}