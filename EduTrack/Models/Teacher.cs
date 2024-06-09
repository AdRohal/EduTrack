using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string P_Pic { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Cin { get; set; }
        public string Address { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string HighestDegree { get; set; }
        public int? YearGraduation { get; set; }
        public string University { get; set; }
        public int Salary { get; set; }
        public string Module { get; set; }
    }
}
