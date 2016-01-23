using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.ViewModels
{
    public class FaultsIndexData
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Fault> Faults { get; set; }
    }
}