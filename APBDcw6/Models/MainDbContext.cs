using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BirthDate).IsRequired();

                p.HasData(
                    new Patient { IdPatient=1, FirstName="Jan", LastName="Kowalski", BirthDate=DateTime.Parse("1990-02-02")},
                    new Patient { IdPatient=2, FirstName="Jan", LastName="Nowak", BirthDate=DateTime.Parse("1991-02-02")}
                        );
            });
            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);
                d.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowal", Email = "jk@wp.pl"},
                    new Doctor { IdDoctor = 2, FirstName = "Jan", LastName = "Nowakski", Email ="jn@o2.pl"}
                        );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();
                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);
                p.HasData(
                    new Prescription { IdPrescription=1, Date=DateTime.Parse("2022-06-10"), DueDate=DateTime.Parse("2022-07-10"), IdDoctor=1, IdPatient=2},
                    new Prescription { IdPrescription=2, Date=DateTime.Parse("2022-06-11"), DueDate=DateTime.Parse("2022-07-11"), IdDoctor=2, IdPatient=1}
                    );
            });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                    new Medicament { IdMedicament = 1, Name = "lekarstwo", Description = "dobre lekarstwo", Type="bardzo dobre lekarstwo" },
                    new Medicament { IdMedicament = 2, Name = "lekarstwo 2", Description = "słabe lekarstwo", Type="nieciekawe lekarstwo" }
                        );
            });

            modelBuilder.Entity<Prescription_Medicament>(p =>
            {
                p.HasKey(e => new { e.IdPrescription, e.IdMedicament});
                p.Property(e => e.Dose).IsRequired();
                p.Property(e => e.Details).HasMaxLength(100);

                p.HasData(
                    new Prescription_Medicament { IdPrescription = 1, IdMedicament = 2, Dose = 3, Details="brać brać" },
                    new Prescription_Medicament { IdPrescription = 2, IdMedicament = 1, Dose = 3, Details="dużo brać" }
                        );
            });
        }
    }
}
