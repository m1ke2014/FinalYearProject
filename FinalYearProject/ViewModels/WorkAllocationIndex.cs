using FinalYearProject.Models;
using System.Collections.Generic;

namespace FinalYearProject.ViewModels
{
    public class WorkAllocationIndex
    {
        public IEnumerable<Staff> StaffMembers { get; set; }
        public IEnumerable<RMA> RMAs { get; set; }
    }
}