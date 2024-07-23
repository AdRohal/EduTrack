using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Models
{
    public class Grade
    {
        public int StudentID { get; set; }
        public int ModuleID { get; set; }
        public decimal GradeValue { get; set; }
    }
}
