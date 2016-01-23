using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public decimal LabourCostPerHour { get; set; }
        public decimal LabourCost { get; set; }
        public int PartsQuantity { get; set; }
        public decimal PartsCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}