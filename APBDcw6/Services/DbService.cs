using APBDcw6.Models;
using APBDcw6.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;
        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ReturnedDoctor>> GetAllDoctors()
        {
            return await _dbContext.Doctors.Select(e => new ReturnedDoctor { FirstName = e.FirstName, LastName = e.LastName, Email = e.Email }).ToListAsync();
        }

        public async Task<ReturnedDoctor> GetDoctor(int id)
        {
            return await _dbContext.Doctors.Where(e => e.IdDoctor == id).Select(e => new ReturnedDoctor { FirstName= e.FirstName, LastName = e.LastName, Email = e.Email}).FirstOrDefaultAsync();
        }
        public async Task DeleteDoctor(int id)
        {
            var doctor = new Doctor() { IdDoctor = id };
            _dbContext.Attach(doctor);
            _dbContext.Remove(doctor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddDoctor(ReturnedDoctor doctor)
        {
            _dbContext.Add(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReturnedPrescription>> GetPrescription(InputPrescription prescription)
        {
            //wybierz pacjenta
            var patient = await _dbContext.Patients.Where(e => 
                e.FirstName.Equals(prescription.ReturnedPatient.FirstName) 
                && e.LastName.Equals(prescription.ReturnedPatient.LastName)
                && e.BirthDate.Equals(prescription.ReturnedPatient.BirthDate)
                ).FirstOrDefaultAsync();
            //wybierz doctora
            var doctor = await _dbContext.Doctors.Where(e =>
                e.FirstName.Equals(prescription.ReturnedDoctor.FirstName)
                && e.LastName.Equals(prescription.ReturnedDoctor.LastName)
                && e.Email.Equals(prescription.ReturnedDoctor.Email)
                ).FirstOrDefaultAsync();
            //wybierz recepty wypisane przez doctora pacjentowi
            var prescriptionsBetweenDoctorAndPatient = await _dbContext.Prescriptions.Where(e => e.IdDoctor == doctor.IdDoctor && e.IdPatient == patient.IdPatient).ToListAsync();
            //wiybierz id lekarstw z zapytania
            List<int> inputMedicaments = new List<int>();
            foreach(InputMedicament m in prescription.InputMedicaments)
            {
                var medicament = await _dbContext.Medicaments.Where(e => e.Description.Equals(m.Description) && e.Name.Equals(m.Name)).FirstOrDefaultAsync();
                inputMedicaments.Add(medicament.IdMedicament);
            }
            //wybierz id lekarstw z recept oraz wszystkie recepty spełniające kryteria zapytania (może być więcej niż 1)
            List<ReturnedPrescription> result = new List<ReturnedPrescription>();
            foreach(Prescription p in prescriptionsBetweenDoctorAndPatient)
            {
                List<int> prescribedMedicamentIDs = new List<int>();
                foreach(Prescription_Medicament pm in p.Prescription_Medicaments)
                {
                    prescribedMedicamentIDs.Add(pm.IdMedicament);
                }
                if (!prescribedMedicamentIDs.Except(inputMedicaments).Any())
                {
                    result.Add(new ReturnedPrescription { Date = p.Date, DueDate = p.DueDate, ReturnedDoctor = prescription.ReturnedDoctor, ReturnedPatient=prescription.ReturnedPatient });
                }
            }
            return result;
        }
    }
}
