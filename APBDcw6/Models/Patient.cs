using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Models
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
