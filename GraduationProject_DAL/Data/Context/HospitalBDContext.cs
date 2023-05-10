using GraduationProject_DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Context
{
    public class HospitalBDContext : DbContext
    {
        public HospitalBDContext(DbContextOptions option) : base(option)
        {

        }
        
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}
