using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class DeclarationOfConformity
    {
        public bool RMACreated { get; set; }
        public int DeclarationOfConformityID { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Customer Address")]
        [StringLength(100)]
        public string CustomerAddress { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        [StringLength(50)]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Customer Number")]
        [DataType(DataType.PhoneNumber)]
        public int ContactNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Company { get; set; }

        public string Position { get; set; }
        public string Site { get; set; }
        public int Number { get; set; }
        [DataType(DataType.PhoneNumber)]

        [Required]
        [Display(Name = "Part Number")]
        [StringLength(20)]
        public string PartNo { get; set; }

        [Required]
        [Display(Name = "Serial Number")]
        [StringLength(20)]
        public string SerialNo { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfInstallation { get; set; }

        [Required]
        [Display(Name = "Equipment Usage")]
        [StringLength(100)]
        public string EquipmentUsage { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string Chemicals { get; set; }

        public int? ChemicalID { get; set; }
        
        public virtual ICollection<Chemical> Chemicals { get; set; }

        [Required]
        [StringLength(20)]
        public string EquipmentCleaned { get; set; }

        [Required]
        [Display(Name = "Decontamination Process")]
        [StringLength(200)]
        public string DecontaminationProcess { get; set; }

        public string OperationTime { get; set; }

        [Required]
        [Display(Name = "Failure Information")]
        [StringLength(200)]
        public string FailureInformation { get; set; }

        [Required]
        [Display(Name = "Part List")]
        [StringLength(100)]
        public string PartList { get; set; }

        public string ActionTaken { get; set; }

        //public int RMAid { get; set; }
        //public virtual RMA RMA { get; set; }
    }
}