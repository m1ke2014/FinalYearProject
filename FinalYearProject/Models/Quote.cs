using System;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public decimal LabourCostPerHour { get; set; }

        [DataType(DataType.Currency)]
        public decimal LabourCost { get; set; }

        public int PartsQuantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal PartsCost { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalPartsCost { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        public int RMAid { get; set; }
        public virtual RMA RMA { get; set; }
        public virtual DeclarationOfConformity DOC { get; set; }
    }
}