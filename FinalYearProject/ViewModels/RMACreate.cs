using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalYearProject.ViewModels
{
    public class RMACreate
    {
        public int RMAID { get; set; }
        public int DOCID { get; set; }
        public string Customer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactName { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Site { get; set; }
        public int Number { get; set; }
        public string PartNo { get; set; }
        public string SerialNo { get; set; }
        public string Description { get; set; }
        public DateTime DateOfInstallation { get; set; }
        public string EquipmentUsage { get; set; }
        public int? ChemicalID { get; set; }
        public string EquipmentCleaned { get; set; }
        public string DecontaminationProcess { get; set; }
        public string OperationTime { get; set; }
        public string FailureInformation { get; set; }
        public string PartList { get; set; }
        public string ActionTaken { get; set; }
        public int TimeTaken { get; set; }
        public string CorrectiveAction { get; set; }
        public string PartsUsed { get; set; }
        public bool RMACreated { get; set; }

        public int Priorityid { get; set; }
        public virtual Priority Priorities { get; set; }

        public int StaffID { get; set; }
        public virtual Staff StaffMembers { get; set; }

        public int StatusID { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<ServiceCall> ServiceCalls { get; set; }
    }
}