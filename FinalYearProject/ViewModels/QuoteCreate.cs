using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.ViewModels
{
    public class QuoteCreate
    {
        public int QuoteID { get; set; }
        public int RMAID { get; set; }
        public int DOCid { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
        public string FailureInformation { get; set; }
        public string CorrectiveAction { get; set; }
        public string PartsUsed { get; set; }
        public int TimeTaken { get; set; }
        public decimal LabourCostPerHour { get; set; }
        public decimal LabourCost { get; set; }
        public int PartsQuantity { get; set; }
        public decimal PartsCost { get; set; }
        public decimal TotalPartsCost { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }
    }
}