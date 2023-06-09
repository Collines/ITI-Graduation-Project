﻿using GraduationProject_DAL.Data.Models;
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
        public virtual DbSet<PatientsLogins> PatientsLogins { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Banners> Banners { get; set; }
        public virtual DbSet<BannerTranslation> BannerTranslations { get; set; }
        public virtual DbSet<CampImage> CampImages { get; set; }
        public virtual DbSet<PatientImage> PatientImages { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<ArticleTranslations> ArticleTranslations { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleImage> ArticleImages { get; set; }
    }
}
