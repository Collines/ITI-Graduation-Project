using GraduationProject_DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Data.Context
{
    public class HospitalDBContext : DbContext
    {
        public HospitalDBContext(DbContextOptions option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>().HasIndex(P => P.Email).IsUnique();
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentTranslations> DepartmentTranslations { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorTranslations> DoctorTranslations { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientTranslations> PatientTranslations { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<PatientRefreshTokens> PatientRefreshTokens { get; set; }
        public virtual DbSet<PatientsLogins> PatientsLogins { get; set; }
    }
}
