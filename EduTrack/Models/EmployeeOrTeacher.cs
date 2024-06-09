using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EduTrack.Models
{
    public class EmployeeOrTeacher
    {
        public string Name { get; set; }
        public string Email{ get; set; }
        public string Phone { get; set; }

        public int Salary { get; set; }
        public string PaymentStatus { get; set; }
        public SolidColorBrush PaymentStatusColor { get; set; }
    }

}
