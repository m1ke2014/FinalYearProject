using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoID { get; set; }
        public string Item { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual ICollection<WorkAllocation> WorkAllocations { get; set; }
    }
}