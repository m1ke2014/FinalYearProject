using System.Collections.Generic;

namespace FinalYearProject.Models
{
    public class Staff //: Person
    {
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + Surname;
            }
        }

        public int RMAid { get; set; }
        public virtual ICollection<RMA> RMAs { get; set; }

        //public virtual JobRole JobRole { get; set; }


    }

}