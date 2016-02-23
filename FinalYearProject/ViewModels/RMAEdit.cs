using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.ViewModels
{
    public class RMAEdit
    {
        public IEnumerable<RMA> RMAs { get; set; }
        public IEnumerable<ServiceCall> ServiceCalls { get; set; }
        public IEnumerable<Priority> Priorities { get; set; }
        public IEnumerable<Staff> StaffMembers { get; set; }
        public IEnumerable<Status> Status { get; set; }
    }
}