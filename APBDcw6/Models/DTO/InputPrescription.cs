using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Models.DTO
{
    public class InputPrescription
    {
        public ReturnedPatient ReturnedPatient { get; set; }
        public ReturnedDoctor ReturnedDoctor { get; set; }
        public IEnumerable<InputMedicament> InputMedicaments { get; set; }
    }
}
