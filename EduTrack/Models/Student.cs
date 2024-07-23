using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string P_Pic { get; set; }
        public string F_Name { get; set; }
        public string M_Name { get; set; }
        public string L_Name { get; set; }
        public string FullName
        {
            get { return $"{F_Name} {L_Name}"; }
        }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? B_Date { get; set; }
        public string Nationality { get; set; }
        public string CIN { get; set; }
        public string Address { get; set; }
        public string CIN_Front_Copy { get; set; }
        public string CIN_Back_Copy { get; set; }
        public string School_Name { get; set; }
        public string Major { get; set; }
        public DateTime? J_Date { get; set; }
        public string High_Degree { get; set; }
        public int Year_Graduation { get; set; }
        public string University { get; set; }
        public string Specialization { get; set; }
        public string Diploma_Copy { get; set; }
        public string Emerg_Contact_F_Name { get; set; }
        public string Emerg_Contact_M_Name { get; set; }
        public string Emerg_Contact_L_Name { get; set; }
        public string Emerg_Contact_Relation { get; set; }
        public string Emerg_Contact_Phone { get; set; }
        public string Emerg_Contact_CIN { get; set; }
        public string Emerg_Contact_CIN_F_Copy { get; set; }
        public string Emerg_Contact_CIN_B_Copy { get; set; }
        public string Class { get; set; }
        public string Grade { get; set; }
    }

}
