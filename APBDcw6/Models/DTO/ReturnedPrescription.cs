using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Models.DTO
{
    public class ReturnedPrescription
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public ReturnedPatient ReturnedPatient { get; set; }
        public ReturnedDoctor ReturnedDoctor { get; set; }
    }
}
