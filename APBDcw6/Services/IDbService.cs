using APBDcw6.Models;
using APBDcw6.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Services
{
    public interface IDbService
    {
        Task<IEnumerable<ReturnedDoctor>> GetAllDoctors();

        Task<ReturnedDoctor> GetDoctor(int id);
        Task DeleteDoctor(int id);
        Task AddDoctor(ReturnedDoctor doctor);
        Task<IEnumerable<ReturnedPrescription>> GetPrescription(InputPrescription prescription);
    }
}
