using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return ClassName;
        }
    }

}
