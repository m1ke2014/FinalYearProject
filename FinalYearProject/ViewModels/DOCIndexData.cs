using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FinalYearProject.ViewModels
{
    public class DOCIndexData
    {
        public IEnumerable<DeclarationOfConformity> DOCs { get; set; }
        public IEnumerable<Chemical> chemicals { get; set; }
    }
}