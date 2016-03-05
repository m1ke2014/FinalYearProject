using FinalYearProject.Models;
using System.Collections.Generic;

namespace FinalYearProject.ViewModels
{
    public class FaultsIndexData
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Fault> Faults { get; set; }
    }
}